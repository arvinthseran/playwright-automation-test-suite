using Framework;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Apcoa.UITests.Hooks;

[Binding]
public class Hooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    [BeforeScenario(Order = 30)]
    public async Task SetUpHelpers()
    {
        await Navigate(UrlConfig.Apcoa_BaseUrl);
    }
}