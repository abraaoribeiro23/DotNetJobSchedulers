using Quartz;
using Serilog;

namespace WebApi.Jobs;

public class RepeatScheduleJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information($"RepeatScheduleJob: {DateTime.Now}");
        await Task.CompletedTask;
    }
}