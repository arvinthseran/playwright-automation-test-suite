namespace Framework.Hooks;

[Binding]
public class PlaywrightHooks(ScenarioContext context)
{
    private static InitializeDriver driver;

    private IBrowserContext browserContext;

    private IBrowser Browser;

    private bool isCloud;

    private static readonly DateTime Date;

    static PlaywrightHooks()
    {
        Date = DateTime.Now;
    }

    [BeforeTestRun]
    public static Task BeforeAll()
    {
        driver = new InitializeDriver();

        return Task.CompletedTask;
    }

    [BeforeScenario(Order = 8)]
    public async Task SetupPlaywrightDriver()
    {
        isCloud = InitializeDriver.isCloud;

        if (isCloud)
            Browser = await driver.IBrowserType.ConnectAsync(CreateCloudDriver());

        else
            Browser = await driver.IBrowserType.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = ["--start-maximized"],
            });

        BrowserNewContextOptions contextOptions;

        if (context.ScenarioInfo.Tags.Contains("mobileapp"))
        {
            string key = (context.ScenarioInfo.Tags.Contains("ios")) ? "iPhone 12 Pro" : "Galaxy S9+";

            var mobileDevice = driver.iPlaywright.Devices[key];

            contextOptions = new BrowserNewContextOptions(mobileDevice);
        }
        else
        {
            contextOptions = new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport,
            };
        }

        browserContext = await Browser.NewContextAsync(new BrowserNewContextOptions(contextOptions));

        if (ShouldTrace())
        {
            await browserContext.Tracing.StartAsync(new()
            {
                Title = context.ScenarioInfo.Title,
                Screenshots = true,
                Snapshots = true
            });
        }

        var page = await browserContext.NewPageAsync();

        context.Set(new Driver(browserContext, page, context.Get<ObjectContext>()));
    }

    [AfterScenario(Order = 98)]
    public async Task StopTracing()
    {
        var pDriver = context.Get<Driver>();

        await context.Get<TryCatchExceptionHelper>().AfterScenarioException(() => pDriver.ScreenshotAsync(true));

        if (ShouldTrace())
        {
            var tracefileName = $"PLAYWRIGHTDATA_{DateTime.Now:HH-mm-ss-fffff}.zip";

            var tracefilePath = $"{context.Get<ObjectContext>().GetDirectory()}/{tracefileName}";

            await context.Get<TryCatchExceptionHelper>().AfterScenarioException(
                async () =>
                {
                    await browserContext.Tracing.StopAsync(new()
                    {
                        Path = tracefilePath
                    });

                    TestContext.AddTestAttachment(tracefilePath, tracefileName);
                }
                );
        }

        if (isCloud)
        {
            if (context.TestError == null)
                await MarkTestStatus("passed", string.Empty, pDriver.Page);
            else
                await MarkTestStatus("failed", context.TestError.Message, pDriver.Page);
        }

        await Browser.CloseAsync();
    }

    public static async Task MarkTestStatus(string status, string reason, IPage page)
    {
        await page.EvaluateAsync("_ => {}", "browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"" + status + "\", \"reason\": \"" + reason + "\"}}");
    }

    private bool ShouldTrace() => context.ScenarioInfo.Tags.Contains("donottracelogin") == false || isCloud == false;

    private string CreateCloudDriver()
    {
        string varbrowserstackusername = Environment.GetEnvironmentVariable("BROWSERSTACKUSER");

        string varbrowserstackaccessKey = Environment.GetEnvironmentVariable("BROWSERSTACKKEY");

        var buildDateTime = Date.ToString("ddMMMyyyy_HH:mm:ss").ToUpper();

        Dictionary<string, string> browserstackOptions = new()
        {
            { "os", "Windows" },
            { "os_version", "11" },
            { "browser", "chrome" },  // allowed browsers are `chrome`, `edge`, `playwright-chromium`, `playwright-firefox` and `playwright-webkit`
            { "browser_version", "latest" },
            { "browserstack.username", varbrowserstackusername },
            { "browserstack.accessKey", varbrowserstackaccessKey },
            { "geoLocation", "FR" },
            { "project", "Playwright Campaingns project" },
            { "build", buildDateTime },
            { "name", context.ScenarioInfo.Title },
            { "buildTag", "playwright" },
            { "resolution", "1280x1024" },
            { "browserstack.local", "false" },
            { "browserstack.localIdentifier", "local_connection_name" },
            { "browserstack.playwrightVersion", "1.latest" },
            { "client.playwrightVersion", "1.latest" },
            { "browserstack.debug", "true" },
            { "browserstack.interactiveDebugging", "true" },
            { "browserstack.console", "info" },
            { "browserstack.networkLogs", "true" }
        };
        string capsJson = JsonConvert.SerializeObject(browserstackOptions);

        return "wss://cdp.browserstack.com/playwright?caps=" + Uri.EscapeDataString(capsJson);
    }
}
