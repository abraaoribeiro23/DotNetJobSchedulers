using Infrastructure.Dkron.Common.Enums.Legacy;

namespace Application.Common.Interfaces;

public interface ISqlDataManager
{
    Task CreateJob(string accessKey, string userGroupId, string orgDbName, string orgConnString, int projectId, string scheduleJobName,
        Guid dataImportScheduleId, DateTime startDate, DateTime endDate, DateTime activeStartTime, DateTime activeEndTime,
        SqlServerJobFrequencyTypes freqType, int freqInterval, SqlServerJobSubDayFrequencyTypes subDayIntervalType, int subDayInterval,
        int freqRelativeInterval, int freqRecurrenceFactor);
}