using Quartz;
using Serilog;

namespace QuartzDotNetSqLite.Jobs;

public class CronScheduleJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information($"CronScheduleJob: {DateTime.Now}");
        await Task.CompletedTask;
    }
}