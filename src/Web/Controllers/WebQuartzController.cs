using Application.Quartz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Quartz;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebQuartzController : ControllerBase
    {
        private readonly ILogger<WebQuartzController> _logger;
        private readonly ISchedulerFactory _schedulerFactory;

        public WebQuartzController(ILogger<WebQuartzController> logger, ISchedulerFactory schedulerFactory)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
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

        [HttpPost("add-simple")]
        public async Task<IActionResult> AddSingleScheduleJob()
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var jobName = Guid.NewGuid().ToString();
            await scheduler.AddSingleScheduleJob(jobName);
            return Ok();
        }
    }
}