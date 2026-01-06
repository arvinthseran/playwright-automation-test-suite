namespace FrameworkHelpers;

public static class Logging
{
    public static void Report(int retryCount, TimeSpan timeSpan, Exception exception, string scenarioTitle, string uniqueIdentifier) => TestContext.Progress.WriteLine(Message(retryCount, timeSpan, exception, scenarioTitle, uniqueIdentifier));

    public static string Message(int retryCount, TimeSpan timeSpan, Exception exception, string scenarioTitle, string uniqueIdentifier)
    {
        return $"{Environment.NewLine}" +
               $"Retry Count : {retryCount}{Environment.NewLine}" +
               $"UniqueIdentifier : {uniqueIdentifier}{Environment.NewLine}" +
               $"TimeSpan : {timeSpan.TotalSeconds} seconds{Environment.NewLine}" +
               $"Scenario Title : {scenarioTitle}{Environment.NewLine}" +
               $"Exception : {exception.Message}{Environment.NewLine}";
    }
}
