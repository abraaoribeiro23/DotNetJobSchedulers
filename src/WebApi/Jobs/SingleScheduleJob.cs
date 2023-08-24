using Quartz;
using Serilog;

namespace WebApi.Jobs;

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