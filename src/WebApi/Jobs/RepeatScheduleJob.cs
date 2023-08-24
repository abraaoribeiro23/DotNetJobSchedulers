using Quartz;
using Serilog;

namespace QuartzDotNetSqLite.Jobs;

public class RepeatScheduleJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information($"RepeatScheduleJob: {DateTime.Now}");
        await Task.CompletedTask;
    }
}