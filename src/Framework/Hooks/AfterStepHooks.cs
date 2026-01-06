namespace Framework;

[Binding]
public class AfterStepHooks(ScenarioContext context)
{
    [AfterStep(Order = 10)]
    public async Task AfterStep()
    {
        string StepOutcome() => context.TestError != null ? "ERROR" : "Done";

        var stepInfo = context.StepContext.StepInfo;

        var objectContext = context.Get<ObjectContext>();

        var message = $"-> {StepOutcome()}: {stepInfo.StepDefinitionType} {stepInfo.Text}";

        objectContext.SetAfterStepInformation(message);

        objectContext.SetDebugInformation(message);

        await Task.CompletedTask;
    }
}