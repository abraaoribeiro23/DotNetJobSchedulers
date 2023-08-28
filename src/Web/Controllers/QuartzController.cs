using Infrastructure.Quartz.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Quartz;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuartzController : ControllerBase
    {
        private readonly ILogger<QuartzController> _logger;
        private readonly ISchedulerFactory _schedulerFactory;

        public QuartzController(ILogger<QuartzController> logger, ISchedulerFactory schedulerFactory)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Get Jobs");

            await using var connection = new SqliteConnection("Data Source=test.db");
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM QRTZ_JOB_DETAILS";
            var result = new List<object>();
            await using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Add(
                        new {
                            SCHED_NAME = reader.GetString(0),
                            JOB_NAME = reader.GetString(1),
                            JOB_GROUP = reader.GetString(2)
                        }
                    );
                }
            }
            return Ok(result);
        }

        [HttpPost("add-repeat")]
        public async Task<IActionResult> AddRepeatScheduleJob(string jobName, double seconds)
        {
            _logger.LogInformation("Add AddRepeatScheduleJob");
            var scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.AddRepeatScheduleJob(jobName, TimeSpan.FromSeconds(seconds));
            return Ok();
        }

        [HttpPost("add-simple")]
        public async Task<IActionResult> AddSingleScheduleJob(string jobName, DateTime startAt, string command)
        {
            _logger.LogInformation("Add AddSingleScheduleJob");
            var scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.AddSingleScheduleJob(jobName, startAt, command);
            return Ok();
        }
    }
}