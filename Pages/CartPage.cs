using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightAssg.Pages
{
    public class CartPage
    {
        private readonly IPage _page;

        public CartPage(IPage page)
        {
            _page = page;
        }

        public async Task<string> GetItemName()
        {
            return await _page.InnerTextAsync(".inventory_item_name");
        }

        public async Task Checkout()
        {
            await _page.ClickAsync("#checkout");
        }
    }
}