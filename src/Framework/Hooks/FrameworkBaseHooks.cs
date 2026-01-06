namespace Framework.Hooks;

public abstract class FrameworkBaseHooks(ScenarioContext context)
{
    protected readonly ScenarioContext context = context;

    protected async Task OpenNewTab()
    {
        var driver = context.Get<Driver>();

        var page = await driver.BrowserContext.NewPageAsync();

        driver.Page = page;
    }

    protected async Task Navigate(string url)
    {
        var driver = context.Get<Driver>();

        context.Get<ObjectContext>().SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }
}
