namespace Framework.Hooks;

[Binding]
public class ObjectContextHooks(ScenarioContext context)
{
    [BeforeScenario(Order = 0)]
    public void SetObjectContext(ObjectContext objectContext) => context.Set(objectContext);
}