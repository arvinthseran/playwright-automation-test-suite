using Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace HS2.UITests.Hooks;

[Binding]
public class Hooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    [BeforeScenario(Order = 30)]
    public async Task SetUpHelpers()
    {
        await Navigate(UrlConfig.Hs2_BaseUrl);

        var driver = context.Get<Driver>();

        var page = driver.Page;

        await page.RouteAsync("**/*.{png,jpg,jpeg}", async route => await route.AbortAsync());

        await page.RouteAsync("**/google-analytics.com/**", async route => await route.AbortAsync());

        var locator = page.GetByRole(AriaRole.Button, new() { Name = "Accept cookies" });

        await page.AddLocatorHandlerAsync(locator, async () => { await locator.ClickAsync(); });
    }
}