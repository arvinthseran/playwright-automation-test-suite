using Framework;
using FrameworkHelpers;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SkillsEngland.UITests.Project.Pages;

public abstract class HeaderPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Banner)).ToContainTextAsync("Qualification finder Apprenticeship finder Occupational maps");
    }
}

public class SkillsEngland_DWPHomePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#content")).ToContainTextAsync("Skills England");
    }

    public async Task<ExploreLocalSkillsAndEmploymentDashboardPage> ExploreLocalSkillsAndEmploymentDashboard()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Explore the local skills" }).ClickAsync();

        return await VerifyPageAsync(() => new ExploreLocalSkillsAndEmploymentDashboardPage(context));
    }
}

public class ExploreLocalSkillsAndEmploymentDashboardPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await page.GetByRole(AriaRole.Heading, new() { Name = "Explore local skills and" }).ClickAsync();

        var texts = await AllTextAsync(page.GetByRole(AriaRole.Heading));

        CollectionAssert.Contains(texts, "Explore local skills and employment data.");
    }

    public async Task<ExploreLocalSkillsDashboardPage> ExploreLocalSkillsDashboard()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Explore local skills data" }).ClickAsync();

        return await VerifyPageAsync(() => new ExploreLocalSkillsDashboardPage(context));
    }
}

public class ExploreLocalSkillsDashboardPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#geoChoice-label")).ToContainTextAsync("Choose an LSIP, CA or England");

        await Assertions.Expect(page.Locator("#titleMap")).ToContainTextAsync("fit in the national picture?");

        await Assertions.Expect(page.Locator("#titleTime")).ToContainTextAsync("rates changing over time?");
    }

    public async Task VerifyMapData(string interest, string location)
    {
        await SelectInterest(interest);

        await SelectLocation(location);

        var data = await page.Locator("#map").Filter(new LocatorFilterOptions { HasTextRegex = new Regex($"{location}Total{interest}:") }).TextContentAsync();

        objectContext.SetDebugInformation(data);

        await Assertions.Expect(page.Locator("#map")).ToContainTextAsync(new Regex($"{location}Total{interest}: [0-9]?[0-9]%"));
    }

    private async Task<ExploreLocalSkillsDashboardPage> SelectLocation(string location)
    {
        await page.Locator("#geoChoice-label").ClickAsync();

        await page.GetByRole(AriaRole.Combobox, new() { Name = "Choose an LSIP, CA or England" }).FillAsync(location);

        await page.Locator(".selectize-dropdown-content").GetByText(location, new() { Exact = true }).Last.ClickAsync();

        return await VerifyPageAsync(() => new ExploreLocalSkillsDashboardPage(context));
    }

    private async Task<ExploreLocalSkillsDashboardPage> SelectInterest(string interest)
    {
        await page.Locator("#splashMetric-label").ClickAsync();

        await page.GetByRole(AriaRole.Listbox).GetByText(interest, new() { Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new ExploreLocalSkillsDashboardPage(context));
    }
}
