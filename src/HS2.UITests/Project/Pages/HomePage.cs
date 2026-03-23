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
        if (IsMobile)
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Menu" }).ClickAsync();
        }
        else
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "What is HS2" }).ClickAsync();
        }

        await page.GetByRole(AriaRole.Link, new() { Name = "What is HS2" }).ClickAsync();

        return await VerifyPageAsync(() => new WhatIsHs2Page(context));
    }

    public async Task<RouteMapPage> GoToRouteMap()
    {
        if (IsMobile)
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Menu" }).ClickAsync();
        }
        else
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Route map" }).ClickAsync();
        }

        await page.GetByLabel("Primary").GetByRole(AriaRole.Link, new() { Name = "Route map" }).ClickAsync();

        return await VerifyPageAsync(() => new RouteMapPage(context));
    }

    public async Task<BuildingHs2Page> GoToBuildingHs2page()
    {
        if (IsMobile)
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Menu" }).ClickAsync();
        }
        else
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Building HS2" }).ClickAsync();
        }

        await page.GetByRole(AriaRole.Link, new() { Name = "Building HS2" }).First.ClickAsync();

        return await VerifyPageAsync(() => new BuildingHs2Page(context));
    }

    public async Task<SupplyChainPage> GoToSupplyChainpage()
    {
        if (IsMobile)
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Menu" }).ClickAsync();
        }
        else
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Supply chain" }).ClickAsync();
        }

        await page.GetByRole(AriaRole.Link, new() { Name = "Supply chain", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new SupplyChainPage(context));
    }

    public async Task<CareersPage> GoToCareerspage()
    {
        if (IsMobile)
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Menu" }).ClickAsync();
        }
        else
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Careers" }).ClickAsync();
        }

        await page.GetByRole(AriaRole.Link, new() { Name = "Careers", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new CareersPage(context));
    }

    public async Task<AboutUsPage> GoToAboutUspage()
    {
        if (IsMobile)
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Menu" }).ClickAsync();
        }
        else
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "About us" }).ClickAsync();
        }

        await page.GetByRole(AriaRole.Link, new() { Name = "About us", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new AboutUsPage(context));
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
        await Assertions.Expect(page.Locator("#map-sidebar-intro--default")).ToContainTextAsync("HS2 route map");
    }

    public override async Task<HomePage> GoToHomePage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Menu" }).ClickAsync();

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

