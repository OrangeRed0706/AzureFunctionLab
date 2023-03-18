using lynn_lab.Helper;
using lynn_lab.Model;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace lynn_lab.Services;

public sealed class TimerTriggerExample
{
    private readonly LineHelper _lineHelper;

    public TimerTriggerExample(LineHelper lineHelper)
    {
        _lineHelper = lineHelper;
    }

    [Function("TimerTriggerExample")]
    public async Task Run([TimerTrigger("0 */5 * * * *")] MyInfo myTimer, FunctionContext context)
    {
        var logger = context.GetLogger("HttpTriggerExample");

        await _lineHelper.SendLineNotify(
            $"C# Timer trigger function executed at: {DateTime.Now},Next timer schedule at: {myTimer.ScheduleStatus.Next.ToString()}");
        logger.LogInformation("C# Timer trigger function executed at: {Now}", DateTime.Now);
        logger.LogInformation("Next timer schedule at: {ScheduleStatusNext}", myTimer.ScheduleStatus.Next);
    }
}