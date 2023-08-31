using Application.Quartz;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Xunit;

namespace Infrastructure.Quartz.Tests;
public class QuartzTest : IClassFixture<ServiceProviderFixture>
{
    private readonly ISchedulerFactory _schedulerFactory;
    public QuartzTest(ServiceProviderFixture fixture)
    {
        _schedulerFactory = fixture.Sp.GetRequiredService<ISchedulerFactory>();
    }

    [Fact]
    public async Task Test()
    {
        var jobName = Guid.NewGuid().ToString();
        var scheduler = await _schedulerFactory.GetScheduler();
        await scheduler.AddSingleScheduleJob(jobName);
        var result = await scheduler.GetJobDetail(JobKey.Create(jobName));
        Assert.NotNull(result);
    }
}