using Microsoft.Extensions.Logging;
using Quartz;

namespace Infrastructure.Quartz.Jobs;

public class SampleScheduleJob : IJob
{
    private readonly ILogger<SampleScheduleJob> _logger;

    public SampleScheduleJob(ILogger<SampleScheduleJob> logger)
    {
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var jobDetail = context.JobDetail.Key.ToString();

        _logger.LogInformation($"{nameof(SampleScheduleJob)}: {DateTime.Now} : {jobDetail}");

        if (!context.MergedJobDataMap.IsEmpty)
        {
            foreach (var keyValuePair in context.MergedJobDataMap.WrappedMap)
            {
                _logger.LogInformation($"{keyValuePair.Key} : {keyValuePair.Value}");
            }
        }

        await Task.CompletedTask;
    }
}