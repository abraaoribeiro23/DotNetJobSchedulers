using Quartz;
using Serilog;

namespace WebApi.Jobs;

public class DailyTimeScheduleJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information($"DailyTimeScheduleJob: {DateTime.Now}");
        await Task.CompletedTask;
    }
}