    using Microsoft.Playwright;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using PlaywrightAssg.Pages;

    namespace PlaywrightAssg.Tests
    {
        public class SauceDemoTest
        {
            private IPlaywright _playwright = null!;
            private IBrowser _browser = null!;
            private IBrowserContext _Context = null!;
            private IPage page = null!;

            [SetUp]
            public async Task Setup()
            {
                _playwright = await Playwright.CreateAsync();
                _browser = await _playwright.Chromium.LaunchAsync(new()
                {
                    Headless = false,
                    SlowMo =500
                });
                _Context = await _browser.NewContextAsync();
                page=await _Context.NewPageAsync();

                await _Context.Tracing.StartAsync(new (){
                    Title = "SauceDemo Test",
                    Screenshots = true,
                    Snapshots =true,
                    Sources = true
                
            });

            }

            [Test]
            public async Task EndToEndTest()
            {
                
                var loginPage = new LoginPage(page);
                var productsPage = new ProductsPage(page);
                var cartPage = new CartPage(page);
                var checkoutPage = new CheckoutPage(page);

                
                await loginPage.Navigate();
                await loginPage.Login("standard_user", "secret_sauce");
                bool loginSuccess = await productsPage.IsLoaded();
                Assert.That(loginSuccess, Is.True, "Login Failed");

                await productsPage.AddFirstTwoItems();

                var count = await productsPage.GetCartCount();
                TestContext.WriteLine($"Cart count after adding items: {count}");
                Assert.That(count, Is.EqualTo("2"));

                await productsPage.RemoveOneItem();

                var updatedCount = await productsPage.GetCartCount();
                TestContext.WriteLine($"Cart count after removing item: {updatedCount}");
                Assert.That(updatedCount, Is.EqualTo("1"));

                await productsPage.GoToCart();

                var item = await cartPage.GetItemName();
                TestContext.WriteLine($"Item in cart: {item}");
                Assert.IsNotNull(item);

                await cartPage.Checkout();

                await checkoutPage.FillInformation("Pratham", "Singh", "400001");
                await checkoutPage.Continue();

                var overviewItem = await checkoutPage.GetOverviewItem();
                TestContext.WriteLine($"Overview item: {overviewItem}");
                Assert.That(overviewItem, Is.EqualTo(item));

                await checkoutPage.Finish();

                var confirmation = await checkoutPage.GetConfirmationMessage();
                Assert.That(confirmation.ToLower(), Does.Contain("thank you for your order"));

                TestContext.WriteLine("TEST COMPLETED SUCCESSFULLY");
            }

            [TearDown]
            public async Task Teardown()
            {
                if (TestContext.CurrentContext.Result.Outcome.Status ==
                    NUnit.Framework.Interfaces.TestStatus.Failed)
                {

                    String tracePath = Directory.GetCurrentDirectory();
                    String traceFolder = Path.Combine(tracePath, "..", "..", "..","Traces");
                    Directory.CreateDirectory(traceFolder);

                    String trace = Path.Combine(traceFolder,$"Trace_{DateTime.Now:yyyyMMdd_HHmmss}.zip");

                    await _Context.Tracing.StopAsync(new()
                    {
                        Path = trace
                    });


                    string projectPath = Directory.GetCurrentDirectory();
                    string folderPath = Path.Combine(projectPath, "..", "..", "..", "Screenshots");

                    Directory.CreateDirectory(folderPath);

                    string filePath = Path.Combine(
                        folderPath,
                        $"Failure_{DateTime.Now:yyyyMMdd_HHmmss}.png"
                    );

                    await page.ScreenshotAsync(new()
                    {
                        Path = filePath,
                        FullPage = true
                    });
                }

                await _browser.CloseAsync();
                _playwright.Dispose();
            }                
        }
}