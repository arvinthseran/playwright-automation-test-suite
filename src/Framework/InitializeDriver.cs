namespace Framework;

public class InitializeDriver
{
    private readonly Task<IBrowserType> _iBrowserType;

    public IBrowserType IBrowserType => _iBrowserType.Result;

    public static BrowserType BrowserType;

    public IPlaywright iPlaywright;

    public static bool isCloud;

    public InitializeDriver()
    {
        _iBrowserType = Task.Run(Initialize);
    }

    public async Task<IBrowserType> Initialize()
    {
        iPlaywright = await Playwright.CreateAsync();

        BrowserType = GetBrowserTypeFromEnv();

        isCloud = BrowserType == BrowserType.Cloud;

        return CreateDriver(isCloud ? BrowserType.Chrome : BrowserType);

    }

    public static BrowserType GetBrowserTypeFromEnv()
    {
        string envBrowserType = Environment.GetEnvironmentVariable("BROWSER_TYPE");

        string browserType = string.IsNullOrEmpty(envBrowserType) ? "Chrome" : envBrowserType;

        if (!Enum.TryParse(browserType, true, out BrowserType type))
            type = BrowserType.Chrome;

        return type;
    }

    public IBrowserType CreateDriver(BrowserType browserType)
    {
        return browserType switch
        {
            BrowserType.Chrome => iPlaywright.Chromium,
            BrowserType.Safari => iPlaywright.Webkit,
            BrowserType.Firefox => iPlaywright.Firefox,
            _ => iPlaywright.Chromium,
        };
    }
}
