using Quartz;
using Serilog;

namespace QuartzDotNetSqLite.Jobs;

public class SingleScheduleJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var cmd = context.MergedJobDataMap.GetString("cmd");
        Log.Information($"SingleScheduleJob: {DateTime.Now}");
        Log.Information($"Command: {cmd}");
        await Task.CompletedTask;
    }
}