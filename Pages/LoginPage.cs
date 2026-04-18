using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightAssg.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
        }

        public async Task Navigate()
        {
            await _page.GotoAsync("https://www.saucedemo.com/");
        }

        public async Task Login(string username, string password)
        {
            await _page.FillAsync("#user-name", username);
            await _page.FillAsync("#password", password);
            await _page.ClickAsync("#login-button");
        }
    }
}