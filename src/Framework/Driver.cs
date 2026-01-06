namespace Framework;

public class Driver(IBrowserContext browserContext, IPage page, ObjectContext objectContext)
{
    public IBrowserContext BrowserContext = browserContext;

    public IPage Page = page;

    private readonly ScreenShotHelper screenShotHelper = new(page, objectContext);

    public void SetPage(IPage _page) => Page = _page;

    public async Task ScreenshotAsync(bool isTestComplete) => await screenShotHelper.ScreenshotAsync(isTestComplete);

    public async Task SelectRowFromTable(string byLinkText, string byKey, ILocator nextPage)
    {
        do
        {
            //var link = page.Locator($"a[href*='{byKey}']:has-text('{byLinkText}')");

            var link = Page.GetByRole(AriaRole.Row, new() { Name = byKey }).GetByRole(AriaRole.Link);

            if (await link.IsVisibleAsync())
            {
                await link.ClickAsync();

                objectContext.SetDebugInformation($"Clicked LinkText - '{byLinkText}' using Key - '{byKey}'");

                break;
            }
            await nextPage.ClickAsync();

        } while (await nextPage.IsVisibleAsync());
    }
}
