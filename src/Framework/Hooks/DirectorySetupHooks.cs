using System.IO;

namespace Framework.Hooks;

[Binding]
public class DirectorySetupHooks(ScenarioContext context, FeatureContext featureContext)
{
    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

    [BeforeScenario(Order = 3)]
    public void SetUpDirectory()
    {
        string directory = GetDirectoryPath();

        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        _objectContext.SetDirectory(directory);
    }

    private string GetDirectoryPath()
    {
        string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestAttachments", $"{DateTime.Now:dd-MM-yyyy}", $"{EscapePatternHelper.DirectoryEscapePattern(featureContext.FeatureInfo.Title)}");

        return Path.GetFullPath(directory);
    }
}