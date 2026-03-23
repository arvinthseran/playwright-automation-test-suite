using Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace HS2.UITests.Project.Pages;

public abstract class HS2BasePage(ScenarioContext context) : BasePage(context)
{
    public virtual async Task<HomePage> GoToHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "High Speed Two (HS2) homepage" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}

public class HomePage(ScenarioContext context) : HS2BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Img, new() { Name = "High Speed Two (HS2) homepage" })).ToBeVisibleAsync();
    }

    public async Task<WhatIsHs2Page> GoToWhatIsHs2Page()
    {
        await ClickMenu("What is HS2");

        await page.GetByRole(AriaRole.Link, new() { Name = "What is HS2" }).ClickAsync();

        return await VerifyPageAsync(() => new WhatIsHs2Page(context));
    }

    public async Task<RouteMapPage> GoToRouteMap()
    {
        await ClickMenu("Route map");

        await page.GetByLabel("Primary").GetByRole(AriaRole.Link, new() { Name = "Route map" }).ClickAsync();

        return await VerifyPageAsync(() => new RouteMapPage(context));
    }

    public async Task<BuildingHs2Page> GoToBuildingHs2page()
    {
        await ClickMenu("Building HS2");

        await page.GetByRole(AriaRole.Link, new() { Name = "Building HS2" }).First.ClickAsync();

        return await VerifyPageAsync(() => new BuildingHs2Page(context));
    }

    public async Task<SupplyChainPage> GoToSupplyChainpage()
    {
        await ClickMenu("Supply chain");

        await page.GetByRole(AriaRole.Link, new() { Name = "Supply chain", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new SupplyChainPage(context));
    }

    public async Task<CareersPage> GoToCareerspage()
    {
        await ClickMenu("Careers");

        await page.GetByRole(AriaRole.Link, new() { Name = "Careers", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new CareersPage(context));
    }

    public async Task<AboutUsPage> GoToAboutUspage()
    {
        await ClickMenu("About us");

        await page.GetByRole(AriaRole.Link, new() { Name = "About us", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new AboutUsPage(context));
    }

    private async Task ClickMenu(string menuName)
    {
        if (IsMobile)
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Menu" }).ClickAsync();
        }
        else
        {
            await page.GetByRole(AriaRole.Button, new() { Name = menuName }).ClickAsync();
        }
    }
}

public class WhatIsHs2Page(ScenarioContext context) : HS2BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is HS2");
    }
}

public class RouteMapPage(ScenarioContext context) : HS2BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Interchange" })).ToBeVisibleAsync();

        await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Birmingham Curzon Street" })).ToBeVisibleAsync();
        
        await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Old Oak Common" })).ToBeVisibleAsync();
        
        await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "London Euston" })).ToBeVisibleAsync();
    }

    public override async Task<HomePage> GoToHomePage()
    {
        if (IsMobile) { await page.GetByRole(AriaRole.Button, new() { Name = "Menu" }).ClickAsync(); }

        await page.GetByRole(AriaRole.Link, new() { Name = "Back to main website" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}

public class BuildingHs2Page(ScenarioContext context) : HS2BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Building HS2");
    }
}

public class SupplyChainPage(ScenarioContext context) : HS2BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Supply chain");
    }
}

public class CareersPage(ScenarioContext context) : HS2BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Careers");
    }
}

public class AboutUsPage(ScenarioContext context) : HS2BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("About us");
    }
}

