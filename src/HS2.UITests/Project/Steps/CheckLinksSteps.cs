using HS2.UITests.Project.Pages;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace HS2.UITests.Project.Steps;

[Binding]
public class HS2LinkSteps(ScenarioContext context)
{
    private HomePage homePage;

    [Given("the user navigates to HS2 home page")]
    public async Task GivenTheUserNavigatesToHS2HomePage()
    {
        homePage = new HomePage(context);

        await homePage.VerifyPage();
    }

    [Then("the user can navigate to What is HS2 page")]
    public async Task ThenTheUserCanNavigateToWhatIsHS2Page()
    {
        var page = await homePage.GoToWhatIsHs2Page();

        homePage = await page.GoToHomePage();
    }

    [Then("the user can navigate to Route map page")]
    public async Task ThenTheUserCanNavigateToRouteMapPage()
    {
        var page = await homePage.GoToRouteMap();

        homePage = await page.GoToHomePage();
    }

    [Then("the user can navigate to Building Hs2 page")]
    public async Task ThenTheUserCanNavigateToBuildingHs2Page()
    {
        var page = await homePage.GoToBuildingHs2page();

        homePage = await page.GoToHomePage();
    }

    [Then("the user can navigate to Supply Chain page")]
    public async Task ThenTheUserCanNavigateToSupplyChainPage()
    {
        var page = await homePage.GoToSupplyChainpage();

        homePage = await page.GoToHomePage();
    }

    [Then("the user can navigate to Careers page")]
    public async Task ThenTheUserCanNavigateToCareersPage()
    {
        var page = await homePage.GoToCareerspage();

        homePage = await page.GoToHomePage();
    }

    [Then("the user can navigate to About us page")]
    public async Task ThenTheUserCanNavigateToAboutUsPage()
    {
        var page = await homePage.GoToAboutUspage();

        homePage = await page.GoToHomePage();
    }
}
