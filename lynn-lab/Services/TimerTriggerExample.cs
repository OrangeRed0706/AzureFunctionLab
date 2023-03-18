using lynn_lab.Helper;
using lynn_lab.Model;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace lynn_lab.Services;

public sealed class TimerTriggerExample
{
    private readonly ILogger _logger;
    private readonly LineHelper _lineHelper;

    public TimerTriggerExample(ILogger logger, LineHelper lineHelper)
    {
        _logger = logger;
        _lineHelper = lineHelper;
    }

    [Function("TimerTriggerExample")]
    public async Task Run([TimerTrigger("0 */5 * * * *")] MyInfo myTimer, FunctionContext context)
    {
        await _lineHelper.SendLineNotify(
            $"C# Timer trigger function executed at: {DateTime.Now},Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        _logger.LogInformation("C# Timer trigger function executed at: {Now}", DateTime.Now);
        _logger.LogInformation("Next timer schedule at: {ScheduleStatusNext}", myTimer.ScheduleStatus.Next);
    }
}