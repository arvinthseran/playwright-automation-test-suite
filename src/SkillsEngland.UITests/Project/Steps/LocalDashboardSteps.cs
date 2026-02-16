using SkillsEngland.UITests.Project.Pages;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SkillsEngland.UITests.Project.Steps;

[Binding]
public class FindAPretSteps(ScenarioContext context)
{
    private ExploreLocalSkillsDashboardPage exploreLocalSkillsDashboardPage;

    [Given("the user navigates to local skills page")]
    public async Task GivenTheUserNavigatesToLocalSkillsPage()
    {
        var page = new SkillsEngland_DWPHomePage(context);

        await page.VerifyPage();

        var page1 = await page.ExploreLocalSkillsAndEmploymentDashboard();

        exploreLocalSkillsDashboardPage = await page1.ExploreLocalSkillsDashboard();
    }


    [Then("the user can load a dashboard for (Employment rate) in (.*)")]
    public async Task ThenTheUserCanLoadADashboard(string interest, string location)
    {
        await exploreLocalSkillsDashboardPage.VerifyMapData(interest, location);
    }
}
