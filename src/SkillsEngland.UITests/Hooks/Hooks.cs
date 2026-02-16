using Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SkillsEngland.UITests.Hooks;

[Binding]
public class Hooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    [BeforeScenario(Order = 30)]
    public async Task SetUpHelpers()
    {
        await Navigate(UrlConfig.SkillsEngland_BaseUrl);

        var driver = context.Get<Driver>();

        var page = driver.Page;

        await page.RouteAsync("**/*.{png,jpg,jpeg}", async route => await route.AbortAsync());

        await page.RouteAsync("**/google-analytics.com/**", async route => await route.AbortAsync());

        var locator = page.GetByRole(AriaRole.Button, new() { Name = "Accept All" });

        await page.AddLocatorHandlerAsync(locator, async () => { await locator.ClickAsync(); });
    }
}