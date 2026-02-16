namespace Framework;

public abstract class BasePage
{
    protected readonly ScenarioContext context;

    protected readonly ObjectContext objectContext;

    protected readonly Driver driver;

    protected readonly RetryHelper retryHelper;

    protected readonly IPage page;

    protected static float LandingPageTimeout => 60000;

    /// <summary>
    /// The result of the asynchronous verification of this instance.
    /// </summary>
    public abstract Task VerifyPage();

    protected BasePage(ScenarioContext context)
    {
        this.context = context;

        objectContext = context.Get<ObjectContext>();

        driver = context.Get<Driver>();

        retryHelper = context.Get<RetryHelper>();

        page = driver.Page;
    }

    protected bool IsMobile => context.ScenarioInfo.Tags.Contains("mobileapp");

    protected static async Task<IReadOnlyList<string>> AllTextAsync(ILocator locator)
    {
        var locators = await locator.AllAsync();

        var alloptions = locators.Select(async l => await l.TextContentAsync());

        var alloptionstexts = await Task.WhenAll(alloptions);

        return alloptionstexts;
    }

    public async Task<T> VerifyPageAsync<T>(Func<T> func) where T : BasePage
    {
        var nextPage = func.Invoke();

        await retryHelper.RetryOnVerifyPage(nextPage.VerifyPage);

        objectContext.SetDebugInformation($"Navigated to page with Title: '{await page.TitleAsync()}'");

        return nextPage;
    }
}
