using Application;
using Infrastructure.Dkron.Common.Enums.Legacy;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Infrastructure.Tests.SqlDataManagerTests;

public class SqlDataManagerTest : IClassFixture<ServiceProviderFixture>
{
    private readonly ISqlDataManager _sqlDataManager;

    public SqlDataManagerTest(ServiceProviderFixture fixture)
    {
        _sqlDataManager = fixture.Sp.GetRequiredService<ISqlDataManager>();
    }

    [Theory]
    [ClassData(typeof(SqlDataManagerTestData))]
    public async Task DynamicTest(SqlServerJobFrequencyTypes freqType, SqlServerJobSubDayFrequencyTypes subDayIntervalType, bool isValid)
    {
        const string accessKey = "access-key";
        const string userGroupId = "user-group";
        const string orgDbName = "org-db-name";
        const int projectId = 1;
        const string scheduleJobName = "schedule-job-name";
        var dataImportScheduleId = Guid.NewGuid();
        var startDate = DateTime.Now;
        const int freqInterval = 1;

        var result = await _sqlDataManager.CreateJob(accessKey, userGroupId, orgDbName, projectId,
            scheduleJobName, dataImportScheduleId, startDate, freqType, freqInterval, subDayIntervalType);

        if (isValid)
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Name);
        }
        else
        {
            Assert.Null(result);
        }
    }
}