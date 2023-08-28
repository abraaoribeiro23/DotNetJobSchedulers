using System.Text;
using Infrastructure.Dkron;
using Infrastructure.Dkron.Common.Enums.Legacy;
using Infrastructure.Dkron.Contracts;
using Microsoft.Extensions.Logging;

namespace Application
{
    public class SqlDataManager : ISqlDataManager
    {
        private readonly IDkronService _dkronService;
        private readonly ILogger<SqlDataManager> _logger;

        public SqlDataManager(IDkronService dkronService, ILogger<SqlDataManager> logger)
        {
            _dkronService = dkronService;
            _logger = logger;
        }
        
        public async Task<JobResponseDto?> CreateJob(string accessKey, string userGroupId, string orgDbName, int projectId, string scheduleJobName,
            Guid dataImportScheduleId, DateTime startDate, SqlServerJobFrequencyTypes freqType, int freqInterval,
            SqlServerJobSubDayFrequencyTypes subDayIntervalType)
        {
            try
            {
                string jobName;

                if (scheduleJobName.Contains("OrgJob_" + orgDbName + "_" + projectId))
                {
                    jobName = scheduleJobName;
                }
                else
                {
                    jobName = "OrgJob_" + orgDbName + "_" + projectId + "_" + scheduleJobName;
                }

                jobName += "_RunOnce";

                var jobExists = await _dkronService.DoesJobExist(jobName);
                if (_logger.IsEnabled(LogLevel.Debug))
                    _logger.LogDebug("The sql job {0} exists or not: {1}", jobName, jobExists);

                if (jobExists)
                {
                    await _dkronService.DeleteJob(jobName);
                }

                var exeCmd = GetExeCommand(accessKey, orgDbName, projectId, userGroupId, dataImportScheduleId);

                var payload = BuildPayload(jobName, exeCmd, freqType, freqInterval, startDate, subDayIntervalType);

                return await _dkronService.CreateJob(payload);
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                    _logger.LogError("Failed to delete job schedule: {0}", ex.Message);
            }
            return null;
        }

        public static JobPayloadDto BuildPayload(string jobName, string exeCmd, SqlServerJobFrequencyTypes freqType,
            int freqInterval, DateTime startDate, SqlServerJobSubDayFrequencyTypes subDayIntervalType)
        {
            var startDateStr = startDate.ToString("yyyyMMdd");

            if (subDayIntervalType != SqlServerJobSubDayFrequencyTypes.SpecifiedTime)
            {
                throw new ArgumentOutOfRangeException(nameof(freqType), freqType, "SqlServerJobSubDayFrequencyTypes Not Supported");
            }

            StringBuilder? schedulerBuild = null;

            //@yearly (or @annually) | Run once a year, midnight, Jan. 1st        | 0 0 0 1 1 *
            //@monthly               | Run once a month, midnight, first of month | 0 0 0 1 * *
            //@weekly                | Run once a week, midnight on Sunday        | 0 0 0 * * 0
            //@daily (or @midnight)  | Run once a day, midnight                   | 0 0 0 * * *
            //@hourly                | Run once an hour, beginning of hour        | 0 0 * * * *
            //@minutely              | Run once a minute, beginning of minute     | 0 * * * * *
            //@manually              | Never runs                                 | N/A

            switch (freqType)
            {
                case SqlServerJobFrequencyTypes.Once:
                    schedulerBuild?.Append($"@at {startDateStr}"); // 2018-01-02T15:04:00Z
                    break;

                case SqlServerJobFrequencyTypes.Daily:
                    schedulerBuild?.Append($"@daily {freqInterval}");
                    break;

                case SqlServerJobFrequencyTypes.Weekly:
                    schedulerBuild?.Append($"@weekly {freqInterval}");
                    break;

                case SqlServerJobFrequencyTypes.Monthly:
                    schedulerBuild?.Append($"@monthly {freqInterval}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(freqType), freqType, "SqlServerJobFrequencyTypes Not Supported");
            }


            var payload = new JobPayloadDto
            {
                Name = jobName,
                Schedule = schedulerBuild?.ToString(),
                Concurrency = "allow",
                Disabled = false,
                Executor = "shell",
                ExecutorConfig = new Dictionary<string, string>
                {
                    {"command","date"}
                },
                Metadata = new Dictionary<string, string>
                {
                    {"exeCmd", exeCmd}
                }
            };

            return payload;
        }

        public static string GetExeCommand(string accessKey, string orgDbName, int projectId, string userGroupId, Guid dataImportScheduleId)
        {
            const string exePath = "application.exe";

            return exePath + " " + accessKey + " d \"" + orgDbName + "\" " + projectId + " " + userGroupId + " " + dataImportScheduleId;
        }
    }
}