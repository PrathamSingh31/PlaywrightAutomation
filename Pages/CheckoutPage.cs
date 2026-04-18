using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightAssg.Pages
{
    public class CheckoutPage
    {
        private readonly IPage _page;

        public CheckoutPage(IPage page)
        {
            _page = page;
        }

        public async Task FillInformation(string first, string last, string zip)
        {
            await _page.FillAsync("#first-name", first);
            await _page.FillAsync("#last-name", last);
            await _page.FillAsync("#postal-code", zip);
        }

        public async Task Continue()
        {
            await _page.ClickAsync("#continue");
        }

        public async Task<string> GetOverviewItem()
        {
            return await _page.InnerTextAsync(".inventory_item_name");
        }

        public async Task Finish()
        {
            await _page.ClickAsync("#finish");
        }

        public async Task<string> GetConfirmationMessage()
        {
            return await _page.InnerTextAsync(".complete-header");
        }
    }
}