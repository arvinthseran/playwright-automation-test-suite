namespace Framework;

public static class VerifyPageHelper
{
    public static async Task<T> VerifyPageAsync<T>(Func<T> func) where T : BasePage
    {
        var nextPage = func.Invoke();

        await nextPage.VerifyPage();

        return nextPage;
    }
}

public abstract class BasePage
{
    protected readonly ScenarioContext context;

    protected readonly ObjectContext objectContext;

    protected readonly Driver driver;

    protected readonly RetryHelper retryHelper;

    protected readonly IPage page;

    protected readonly string[] tags;

    protected static float LandingPageTimeout => 60000;

    /// <summary>
    /// The result of the asynchronous verification of this instance.
    /// </summary>
    public abstract Task VerifyPage();

    protected BasePage(ScenarioContext context)
    {
        this.context = context;

        tags = context.ScenarioInfo.Tags;

        objectContext = context.Get<ObjectContext>();

        driver = context.Get<Driver>();

        retryHelper = context.Get<RetryHelper>();

        page = driver.Page;
    }

    protected async Task VerifyAnySections(string locator, string taskName, string status)
    {
        var items = await AllTextAsync(locator);

        items = [.. items.ToList().Select(x => x.RemoveMultipleSpace().TrimStart().TrimEnd())];

        objectContext.SetDebugInformation($"{items.ToString(Environment.NewLine)}");

        var item = items.ToList().FirstOrDefault(x => x.ContainsCompareCaseInsensitive(taskName));

        StringAssert.Contains(status, item);
    }

    protected async Task<IReadOnlyList<string>> AllTextAsync(string locator)
    {
        var locators = await page.Locator(locator).AllAsync();

        var alloptions = locators.Select(async l => await l.TextContentAsync());

        var alloptionstexts = await Task.WhenAll(alloptions);

        return alloptionstexts;
    }

    protected async Task Navigate(string url)
    {
        var driver = context.Get<Driver>();

        context.Get<ObjectContext>().SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }

    public async Task<T> VerifyPageAsync<T>(Func<T> func) where T : BasePage
    {
        var nextPage = func.Invoke();

        await nextPage.VerifyPage();

        objectContext.SetDebugInformation($"Navigated to page with Title: '{await page.TitleAsync()}'");

        return nextPage;
    }

    protected async Task AcceptAllCookiesIfVisible()
    {
        var cookieLocator = page.GetByRole(AriaRole.Button, new() { Name = "Accept all cookies" });

        if (await cookieLocator.CountAsync() > 0 && await cookieLocator.IsVisibleAsync())
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Accept all cookies" }).ClickAsync();
        }
    }

    protected async Task<IResponse> ReloadPageAsync()
    {
        objectContext.SetDebugInformation($"Reload page with Title: '{await page.TitleAsync()}'");

        return await page.ReloadAsync();
    }

    protected void VerifyPage(IReadOnlyList<string> actual, string expected)
    {
        if (actual.Any(x => x.ContainsCompareCaseInsensitive(expected)))
        {
            objectContext.SetDebugInformation(MessageHelper.OutputMessage("Verified page", [expected], actual));

            return;
        }

        throw new Exception(MessageHelper.GetExceptionMessage("Page", expected, actual));
    }

    protected async Task SelectRandomRadioOption()
    {
        var options = RandomDataGenerator.GetRandom(await page.GetByRole(AriaRole.Radio).AllAsync());

        await options.CheckAsync();
    }
}
