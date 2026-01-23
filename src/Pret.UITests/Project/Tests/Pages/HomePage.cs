using Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Pret.UITests.Project.Tests.Pages;

public abstract class HeaderPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        var locator = page.GetByRole(AriaRole.Banner, new() { Name = "Header" });

        await Assertions.Expect(locator).ToBeVisibleAsync();

        await Assertions.Expect(locator).ToContainTextAsync("Pret A Manger");
    }
}

public class HomePage(ScenarioContext context) : HeaderPage(context)
{
    public override async Task VerifyPage()
    {
        await base.VerifyPage();

        await page.GetByRole(AriaRole.Link, new() { Name = "Pret A Manger homepage" }).ClickAsync();

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("It’s comfort food season", new LocatorAssertionsToContainTextOptions { Timeout = 10000 });
    }

    public async Task<FindAPretPage> GoToFindAPret()
    {
        if (IsMobile)
        {
            await page.GetByRole(AriaRole.Checkbox, new() { Name = "Open navigation" }).CheckAsync();
        }

        await page.GetByTestId("navigation-hamburger").GetByRole(AriaRole.Link, new() { Name = "Find A Pret" }).ClickAsync();

        return await VerifyPageAsync(() => new FindAPretPage(context));
    }
}

public class FindAPretPage(ScenarioContext context) : HeaderPage(context)
{
    public override async Task VerifyPage()
    {
        await base.VerifyPage();

        await Assertions.Expect(page.GetByRole(AriaRole.Combobox, new() { Name = "Search locations" })).ToBeVisibleAsync();
    }

    public async Task<LocationPage> FilterLocations(string location)
    {
        await page.GetByRole(AriaRole.Combobox, new() { Name = "Search locations" }).ClickAsync();

        await page.GetByRole(AriaRole.Combobox, new() { Name = "Search locations" }).FillAsync(location);

        await page.GetByRole(AriaRole.Option, new() { Name = "Pret A Manger" }).First.ClickAsync();

        if (IsMobile)
        {
            await page.GetByRole(AriaRole.Tab, new() { Name = "Map view" }).ClickAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Map pin" }).Last.ClickAsync();
        }

        await page.GetByRole(AriaRole.Link, new() { Name = "Detail Page" }).First.ClickAsync();

        return await VerifyPageAsync(() => new LocationPage(context));
    }
}

public class LocationPage(ScenarioContext context) : HeaderPage(context)
{
    public override async Task VerifyPage()
    {
        await base.VerifyPage();

        await Assertions.Expect(page.GetByTestId("details-box-top").GetByRole(AriaRole.Heading)).ToContainTextAsync("Pret A Manger");
    }
}
