using Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Apcoa.UITests.Project.Tests.Pages;

public class ParkingHomePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("APCOA Connect is parking made simple");

    public async Task<ParkingHomePage> AcceptCookie()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Accept All" }).ClickAsync();

        return await VerifyPageAsync(() => new ParkingHomePage(context));
    }

    public async Task<PickYourSpace> GoToPickYourSpace(string space)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "City, street or postcode City" }).ClickAsync();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "City, street or postcode City" }).FillAsync(space);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        return await VerifyPageAsync(() => new PickYourSpace(context));
    }
}


public class PickYourSpace(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("[id=\"c\"]")).ToContainTextAsync("Pick your");
    }

    public async Task<LocationPage> GoToLocationPage()
    {
        await page.Locator("ui-carpark-result-item a").First.ClickAsync();

        return await VerifyPageAsync(() => new LocationPage(context));
    }
}

public class LocationPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("location");
    }

    public async Task<ReviewYourSpace> GoToReviewYourSpace()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Purchase today" }).ClickAsync();

        return await VerifyPageAsync(() => new ReviewYourSpace(context));
    }

}

public class ReviewYourSpace(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Main)).ToContainTextAsync("Confirm your selection");

        var locator = page.Locator("#AcceptWarning");

        if (await locator.IsVisibleAsync()) await locator.ClickAsync();
    }
}