namespace Framework.Hooks;

[Binding]
public class FrameworkHelpersHooks
{
    private readonly ScenarioContext _context;
    private readonly string[] _tags;

    public FrameworkHelpersHooks(ScenarioContext context) { _context = context; _tags = _context.ScenarioInfo.Tags; }

    [BeforeScenario(Order = 2)]
    public void SetUpFrameworkHelpers()
    {
        var objectContext = _context.Get<ObjectContext>();

        _context.Set(new TryCatchExceptionHelper(objectContext));

        objectContext.SetTestDataList(_tags);

        _context.Set(new RetryHelper(_context.ScenarioInfo, objectContext));
    }
}