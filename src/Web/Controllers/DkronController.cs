using Infrastructure.Dkron;
using Infrastructure.Dkron.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DkronController : ControllerBase
    {
        private readonly ILogger<DkronController> _logger;
        private readonly IDkronService _dkronService;

        public DkronController(ILogger<DkronController> logger, IDkronService dkronService)
        {
            _logger = logger;
            _dkronService = dkronService;
        }
        
        [HttpPost("add-simple")]
        public async Task<IActionResult> AddSingleScheduleJob([FromBody] JobPayloadDto dto)
        {
            _logger.LogInformation("DkronController::AddSingleScheduleJob([...])");
            var result = await _dkronService.CreateJob(dto);
            return Ok(result);
        }
    }
}