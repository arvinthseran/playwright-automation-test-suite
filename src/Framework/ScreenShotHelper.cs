namespace Framework;

public class ScreenShotHelper(IPage page, ObjectContext objectContext)
{
    private readonly ScreenShotTitleGenerator screenShotTitleGenerator = new();

    public class ScreenShotTitleGenerator
    {
        private int start;

        public ScreenShotTitleGenerator() => start = 0;

        public string GetPrefix() => $"{++start:D2}_{DateTime.Now:fffff}";
    }

    public async Task ScreenshotAsync(bool isTestComplete = false)
    {
        string scenarioTitle = isTestComplete ? "TestCompletedOnThisPage" : $"{screenShotTitleGenerator.GetPrefix()}";

        (string screenshotPath, string imageName) = new WindowsFileHelper().GetFileDetails(objectContext.GetDirectory(), $"{DateTime.Now:HH-mm-ss}_{scenarioTitle}.png".RemoveSpace());

        await ScreenshotAsync(screenshotPath, imageName);
    }

    private async Task ScreenshotAsync(string screenshotPath, string imageName)
    {
        objectContext.SetDebugInformation($"screenshot taken - {imageName}");

        await page.ScreenshotAsync(new()
        {
            Path = screenshotPath,
            FullPage = true,
        });

        TestContext.AddTestAttachment(screenshotPath, imageName);
    }
}
