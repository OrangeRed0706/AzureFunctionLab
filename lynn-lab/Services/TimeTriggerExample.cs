﻿using System;
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
    public async Task Run([TimerTrigger("0 */10 * * * *")] MyInfo myTimer, FunctionContext context)
    {
        var utcTime = DateTimeOffset.UtcNow.AddHours(8);
        var logger = context.GetLogger("TimeTriggerExample1");

        await _lineHelper.SendLineNotify(
            $"\n C# Timer trigger function executed at: {utcTime},\n Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        var option = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        logger.LogInformation("MyInfo: {Serialize}", JsonSerializer.Serialize(myTimer, option));
        logger.LogInformation("Function Context: {Serialize}", JsonSerializer.Serialize(context));
        logger.LogInformation("C# Timer trigger function executed at: {LocalTime})", utcTime);
        logger.LogInformation("Next timer schedule at: {LocalTime}", myTimer.ScheduleStatus.Next);
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