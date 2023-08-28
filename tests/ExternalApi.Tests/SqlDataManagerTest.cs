using Application;
using Infrastructure.Dkron.Common.Enums.Legacy;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Infrastructure.Tests;

public class SqlDataManagerTest : IClassFixture<ServiceProviderFixture>
{
    private readonly ISqlDataManager _sqlDataManager;

    public SqlDataManagerTest(ServiceProviderFixture fixture)
    {
        _sqlDataManager = fixture.Sp.GetRequiredService<ISqlDataManager>();
    }

    [Fact]
    public async Task Test()
    {
        const string accessKey = "access-key";
        const string userGroupId = "user-group";
        const string orgDbName = "org-db-name";
        const int projectId = 1;
        const string scheduleJobName = "schedule-job-name";
        var dataImportScheduleId = Guid.NewGuid();
        var startDate = DateTime.Now;
        const SqlServerJobFrequencyTypes freqType = SqlServerJobFrequencyTypes.Daily;
        const int freqInterval = 1;
        const SqlServerJobSubDayFrequencyTypes subDayIntervalType = SqlServerJobSubDayFrequencyTypes.Hours;

        var result = await _sqlDataManager.CreateJob(accessKey, userGroupId, orgDbName, projectId,
            scheduleJobName, dataImportScheduleId, startDate, freqType, freqInterval, subDayIntervalType);

        Assert.NotNull(result);
        Assert.NotNull(result.Name);
    }
}