using System;
using System.Text.Json;
using lynn_lab.Helper;
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
    public async Task Run([TimerTrigger("0 */1 * * * *")] MyInfo myTimer, FunctionContext context)
    {
        var utcTime = DateTime.UtcNow;
        var localTime = ToTaipeiTime(utcTime);
        var logger = context.GetLogger("TimeTriggerExample1");

        await _lineHelper.SendLineNotify(
            $"\n C# Timer trigger function executed at: {localTime}");
        var option = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        // logger.LogInformation($"MyInfo: {JsonSerializer.Serialize(myTimer, option)}");
        // logger.LogInformation($"Function Context: {JsonSerializer.Serialize(context)}");
        logger.LogInformation("C# Timer trigger function executed at: {LocalTime})", localTime);
        // logger.LogInformation("Next timer schedule at: {LocalTime}", ToTaipeiTime(myTimer.ScheduleStatus.Next));
    }

    private DateTime ToTaipeiTime(DateTime time)
    {
        var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time"); // 指定時區
        return TimeZoneInfo.ConvertTimeFromUtc(time, timeZoneInfo);
    }
}

public class MyInfo
{
    public MyScheduleStatus ScheduleStatus { get; set; }

    public bool IsPastDue { get; set; }
}

public class MyScheduleStatus
{
    public DateTime Last { get; set; }

    public DateTime Next { get; set; }

    public DateTime LastUpdated { get; set; }
}