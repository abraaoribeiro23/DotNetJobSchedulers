﻿using Quartz;
using Serilog;

namespace QuartzDotNetSqLite.Jobs;

public class DailyTimeScheduleJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information($"DailyTimeScheduleJob: {DateTime.Now}");
        await Task.CompletedTask;
    }
}