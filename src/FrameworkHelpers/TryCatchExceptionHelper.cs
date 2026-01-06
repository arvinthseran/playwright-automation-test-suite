namespace FrameworkHelpers;

public class TryCatchExceptionHelper(ObjectContext objectContext)
{
    public void AfterScenarioException(Action action)
    {
        try
        {
            action.Invoke();
        }
        catch (Exception ex)
        {
            SetAfterScenarioException(ex);
        }
    }

    public async Task AfterScenarioException(Func<Task> func)
    {
        try
        {
            await func.Invoke();
        }
        catch (Exception ex)
        {
            SetAfterScenarioException(ex);
        }
    }

    private void SetAfterScenarioException(Exception ex) => objectContext.SetAfterScenarioException(ex);
}
