using Apcoa.UITests.Project.Tests.Pages;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Apcoa.UITests.Project.Tests.Steps;

[Binding]
public class BookingSteps(ScenarioContext context)
{

    [Given("the user navigates to Home page")]
    public async Task GivenTheUserNavigatesToHomePage()
    {
        var page = new ParkingHomePage(context);

        await page.AcceptCookie();

        await page.VerifyPage();
    }

    [Then("the user book a parking near (.*)")]
    public async Task ThenTheUserBookAParkingNear(string space)
    {
        var page = new ParkingHomePage(context);

        var page1 = await page.GoToPickYourSpace(space);

        var page2 = await page1.GoToLocationPage();

        await page2.GoToReviewYourSpace();
    }

}
