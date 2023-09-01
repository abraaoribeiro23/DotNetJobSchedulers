using Application.Common.Interfaces;
using Infrastructure.Dkron;
using Infrastructure.Dkron.Common.Enums.Legacy;
using Infrastructure.Dkron.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Dkron
{
    public class DkronDataManager : IQuartzDataManager
    {
        private readonly IDkronService _dkronService;
        private readonly ILogger<DkronDataManager> _logger;
        private const string JobNamePrefix = "org-job";
        private const string JobNameSeparator = "_";

        public DkronDataManager(IDkronService dkronService, ILogger<DkronDataManager> logger)
        {
            _dkronService = dkronService;
            _logger = logger;
        }

        public async Task CreateJob(string accessKey, string userGroupId, string orgDbName, string orgConnString, int projectId, string scheduleJobName,
            Guid dataImportScheduleId, DateTime startDate, DateTime endDate, DateTime activeStartTime, DateTime activeEndTime,
            SqlServerJobFrequencyTypes freqType, int freqInterval, SqlServerJobSubDayFrequencyTypes subDayIntervalType, int subDayInterval,
            int freqRelativeInterval, int freqRecurrenceFactor)
        {
            try
            {
                var jobName = GetJobName(orgDbName, projectId, scheduleJobName);
                _logger.LogInformation("Creating Job: {0}", jobName);

                var jobSaved = await _dkronService.GetJobByName(jobName);
                var jobExists = jobSaved != null;

                if (jobExists)
                {
                    if (_logger.IsEnabled(LogLevel.Debug))
                        _logger.LogDebug("Job {0} already exists", jobName);
                    await _dkronService.DeleteJobByName(jobName);
                }

                var body = new { accessKey, orgDbName, projectId, userGroupId, dataImportScheduleId };

                var request = new DkronHttpExecutorConfigDto
                {
                    Method = "POST",
                    Url = "",
                    Headers = "[\"Content-Type: application/json\"]",
                    Body = JsonConvert.SerializeObject(body),
                    Timeout = "100",
                    ExpectCode = "200",
                    ExpectBody = "",
                    Debug = "true",
                };

                var payload = BuildPayload(jobName, freqType, freqInterval, startDate, activeStartTime, activeEndTime,
                    subDayIntervalType, subDayInterval, freqRelativeInterval, freqRecurrenceFactor);
                payload.ExecutorConfig = request;

                await _dkronService.CreateJob(payload);
                _logger.LogInformation("The job was successfully created: Job Name {0}", jobName);
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                    _logger.LogError("Failed to Create Job Schedule: {0}", ex);
            }
        }

        private static string GetJobName(string orgDbName, int projectId, string scheduleJobName)
        {
            if (scheduleJobName.Contains(JobNamePrefix + JobNameSeparator + orgDbName + JobNameSeparator + projectId))
            {
                return scheduleJobName;
            }

            return JobNamePrefix + JobNameSeparator + orgDbName + JobNameSeparator + projectId + JobNameSeparator + scheduleJobName;
        }

        public static DkronHttpJobPayloadDto BuildPayload(string jobName, SqlServerJobFrequencyTypes freqType, int freqInterval, DateTime startDate,
            DateTime activeStartTime, DateTime? activeEndTime, SqlServerJobSubDayFrequencyTypes subDayIntervalType, int? subDayInterval,
            int? freqRelativeInterval, int? freqRecurrenceFactor)
        {
            var schedule = DkronScheduleHelper.GetSchedule(freqType, freqInterval, startDate, activeStartTime, activeEndTime,
                subDayIntervalType, subDayInterval, freqRelativeInterval, freqRecurrenceFactor);

            var payload = new DkronHttpJobPayloadDto
            {
                Name = jobName,
                Schedule = schedule,
                Disabled = false
            };

            return payload;
        }
    }
}