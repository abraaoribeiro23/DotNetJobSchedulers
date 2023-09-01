using Infrastructure.Dkron;
using Infrastructure.Dkron.Contracts.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebDkronController : ControllerBase
    {
        private readonly ILogger<WebDkronController> _logger;
        private readonly IDkronService _dkronService;

        public WebDkronController(ILogger<WebDkronController> logger, IDkronService dkronService)
        {
            _logger = logger;
            _dkronService = dkronService;
        }
        
        [HttpPost("add-simple")]
        public async Task<IActionResult> AddSingleScheduleJob([FromBody] DkronJobPayload dto)
        {
            var result = await _dkronService.CreateJob(dto);
            return Ok(result);
        }
    }
}