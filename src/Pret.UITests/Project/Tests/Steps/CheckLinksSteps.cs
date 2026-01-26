using Pret.UITests.Project.Tests.Pages;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Pret.UITests.Project.Tests.Steps;

[Binding]
public class FindAPretSteps(ScenarioContext context)
{
    [Given("the user navigates to Home page")]
    public async Task GivenTheUserNavigatesToHomePage()
    {
        var page = new HomePage(context);

        await page.VerifyPage();
    }

    [Then("the user can find a pret near (.*)")]
    public async Task ThenTheUserCanFindAPret(string location)
    {
        var page = new HomePage(context);

        var page1 = await page.GoToFindAPret();

        var page2 = await page1.FilterLocations(location);

        await page2.VerifyPage();
    }
}
