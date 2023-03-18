using System.Text.Json;
using lynn_lab.Helper;
using lynn_lab.Model;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace lynn_lab.Services;

public class TimeTriggerExample
{
    private readonly LineHelper _lineHelper;

    public TimeTriggerExample(LineHelper lineHelper)
    {
        _lineHelper = lineHelper;
    }

    [Function("TimeTriggerExample")]
    public async Task Run([TimerTrigger("0 */10 * * * *")] MyInfo myTimer, FunctionContext context)
    {
        var logger = context.GetLogger("TimeTriggerExample1");
        await _lineHelper.SendLineNotify(
            $"C# Timer trigger function executed at: {DateTime.Now},\n Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        var option = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        logger.LogInformation("MyInfo: {MyInfo}", JsonSerializer.Serialize(myTimer, option));
        logger.LogInformation("Function Context: {Context}", JsonSerializer.Serialize(context));
        logger.LogInformation("C# Timer trigger function executed at: {Now}", DateTime.Now.ToLocalTime());
        logger.LogInformation("Next timer schedule at: {ScheduleStatusNext}",
            myTimer.ScheduleStatus.Next.ToLocalTime());
    }
}