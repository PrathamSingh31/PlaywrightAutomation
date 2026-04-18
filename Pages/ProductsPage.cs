using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightAssg.Pages
{
    public class ProductsPage
    {
        private readonly IPage _page;

        public ProductsPage(IPage page)
        {
            _page = page;
        }

        public async Task AddFirstTwoItems()
        {
            await _page.GetByRole(AriaRole.Button, new() { Name = "Add to cart" }).Nth(0).ClickAsync();
            await _page.GetByRole(AriaRole.Button, new() { Name = "Add to cart" }).Nth(2).ClickAsync();
        }

        public async Task<string> GetCartCount()
        {
            return await _page.InnerTextAsync(".shopping_cart_badge");
        }

        public async Task RemoveOneItem()
        {
            await _page.GetByRole(AriaRole.Button, new() { Name = "Remove" }).Nth(0).ClickAsync();
        }

        public async Task GoToCart()
        {
            await _page.ClickAsync(".shopping_cart_link");
        }

        public async Task<bool> IsLoaded()
        {
            return await _page.Locator(".inventory_list").IsVisibleAsync();
        }
    }
}