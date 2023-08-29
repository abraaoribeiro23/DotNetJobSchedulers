using Infrastructure.Dkron.Common.Enums.Legacy;
using Infrastructure.Dkron.Contracts.Base;

namespace Application;

public interface ISqlDataManager
{
    Task<DkronJobResponse?> CreateJob(string accessKey, string userGroupId,
        string orgDbName, string orgConnString, int projectId, string scheduleJobName, Guid dataImportScheduleId,
        DateTime startDate, DateTime endDate, DateTime activeStartTime, DateTime activeEndTime,
        SqlServerJobFrequencyTypes freqType, int freqInterval, SqlServerJobSubDayFrequencyTypes subDayIntervalType, int subDayInterval,
        int freqRelativeInterval, int freqRecurrenceFactor);
}