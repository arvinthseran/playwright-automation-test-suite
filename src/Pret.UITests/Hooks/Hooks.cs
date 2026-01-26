using Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Pret.UITests.Hooks;

[Binding]
public class Hooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    [BeforeScenario(Order = 30)]
    public async Task SetUpHelpers()
    {
        await Navigate(UrlConfig.Pret_BaseUrl);

        var driver = context.Get<Driver>();

        var page = driver.Page;

        var locator = page.GetByRole(AriaRole.Button, new() { Name = "Accept All" });

        await page.AddLocatorHandlerAsync(locator, async () => { await locator.ClickAsync(); });
    }
}