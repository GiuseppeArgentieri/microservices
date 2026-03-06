using Microsoft.AspNetCore.Mvc;

namespace Scheduler.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        [HttpGet("ping", Name = "Ping")]
        public ActionResult<string> Get()
        {
            _logger.LogInformation("Ping received at {Time}", DateTime.UtcNow);
            return Ok("Pong");
        }
    }
}
