using SkillsEngland.UITests.Project.Pages;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SkillsEngland.UITests.Project.Steps;

[Binding]
public class AreaSummaryDashboardSteps(ScenarioContext context)
{
    private ExploreAreaSummaryDashboardPage exploreAreaSummaryDashboardPage;


    [Given("the user navigates to area summary dashboard page")]
    public async Task GivenTheUserNavigatesToAreaSummaryDashboardPage()
    {
        var page = new SkillsEngland_DWPHomePage(context);

        await page.VerifyPage();

        var page1 = await page.ExploreLocalSkillsAndEmploymentDashboard();

        exploreAreaSummaryDashboardPage = await page1.GoToAreaSummary();
    }



    [Then("the user can load an area summary dashboard for (.*)")]
    public async Task ThenTheUserCanLoadAnAreaSummaryDashboard(string location)
    {
        await exploreAreaSummaryDashboardPage.VerifyMapData(location);
    }

}
