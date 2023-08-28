using Infrastructure.Dkron.Common.Enums.Legacy;
using Infrastructure.Dkron.Contracts;

namespace Application;

public interface ISqlDataManager
{
    Task<JobResponseDto?> CreateJob(string accessKey, string userGroupId, string orgDbName, int projectId, string scheduleJobName,
        Guid dataImportScheduleId, DateTime startDate, SqlServerJobFrequencyTypes freqType, int freqInterval,
        SqlServerJobSubDayFrequencyTypes subDayIntervalType);
}