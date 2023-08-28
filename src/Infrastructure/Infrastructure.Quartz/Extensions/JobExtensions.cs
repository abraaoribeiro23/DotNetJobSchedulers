using Infrastructure.Quartz.Jobs;
using Quartz;

namespace Infrastructure.Quartz.Extensions;

public static class JobExtensions
{
    private const string GroupPrefix = "_group";
    private const string TriggerPrefix = "_trigger";

    public static async Task AddSingleScheduleJob(this IScheduler scheduler, string jobName, DateTime startAt, string command)
    {
        var groupName = jobName + GroupPrefix;
        var triggerName = jobName + TriggerPrefix;

        var job = JobBuilder.Create<SampleScheduleJob>()
            .WithIdentity(jobName, groupName)
            .UsingJobData("cmd", command)
            .Build();
        var trigger = TriggerBuilder.Create()
            .WithIdentity(triggerName, groupName)
            .StartAt(new DateTimeOffset(startAt)) // Ex.: new DateTime(2023, 02, 28, 13, 02, 00)
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }

    public static async Task AddRepeatScheduleJob(this IScheduler scheduler, string jobName, TimeSpan interval)
    {
        var groupName = jobName + GroupPrefix;
        var triggerName = jobName + TriggerPrefix;

        var job = JobBuilder.Create<SampleScheduleJob>()
            .WithIdentity(jobName, groupName)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity(triggerName, groupName)
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithInterval(interval)
                .RepeatForever())
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }

    public static async Task AddDailyTimeScheduleJob(this IScheduler scheduler, string jobName, TimeOfDay startingDailyAt, TimeOfDay endingDailyAt,
        TimeSpan interval)
    {
        var groupName = jobName + GroupPrefix;

        var job = JobBuilder.Create<SampleScheduleJob>()
            .WithIdentity(jobName, groupName)
            .Build();
        var trigger = TriggerBuilder.Create()
            .WithDailyTimeIntervalSchedule(s =>
                s.OnEveryDay()
                    .StartingDailyAt(startingDailyAt)
                    .EndingDailyAt(endingDailyAt))
            .WithSimpleSchedule(x => x
                .WithInterval(interval))
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }

    public static async Task AddCronScheduleJob(this IScheduler scheduler, string jobName, string cronSchedule)
    {
        var groupName = jobName + GroupPrefix;
        var triggerName = jobName + TriggerPrefix;

        var job = JobBuilder.Create<SampleScheduleJob>()
            .WithIdentity(jobName, groupName)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity(triggerName, groupName)
            .WithCronSchedule(cronSchedule) // Ex.: "0 59 14 * * ?"
            .ForJob(jobName, groupName)
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }
}