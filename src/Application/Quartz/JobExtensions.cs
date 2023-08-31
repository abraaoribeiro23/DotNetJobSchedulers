using Application.Quartz.Jobs;
using Quartz;

namespace Application.Quartz;

public static class JobExtensions
{
    private const string GroupPrefix = "_group";
    private const string TriggerPrefix = "_trigger";

    public static async Task AddSingleScheduleJob(this IScheduler scheduler, string jobName)
    {
        var cmd = Guid.NewGuid().ToString();
        var startAt = DateTime.Now;
        var groupName = jobName + GroupPrefix;
        var triggerName = jobName + TriggerPrefix;

        var job = JobBuilder.Create<SampleScheduleJob>()
            .WithIdentity(jobName, groupName)
            .Build();
        var trigger = TriggerBuilder.Create()
            .WithIdentity(triggerName, groupName)
            .StartAt(new DateTimeOffset(startAt).AddSeconds(20)) // Ex.: new DateTime(2023, 02, 28, 13, 02, 00)
            .EndAt(new DateTimeOffset(startAt).AddDays(2))
            .WithDailyTimeIntervalSchedule(builder =>
            {
                builder.StartingDailyAt(new TimeOfDay(startAt.Hour, startAt.Minute, startAt.Second));
                builder.EndingDailyAt(new TimeOfDay(startAt.Hour, startAt.Minute+2, startAt.Second));
                builder.WithIntervalInSeconds(10);
            })
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }
}