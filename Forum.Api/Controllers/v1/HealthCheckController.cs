using Forum.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/health")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IHealthCheckService _healthCheck;
        public HealthCheckController(IHealthCheckService healthCheck)
        {
            _healthCheck = healthCheck;
        }

        [HttpGet]
        public async Task<IActionResult> GetHealth(CancellationToken cancellationToken)
        {
            var result = await _healthCheck.CheckDatabaseHealth(cancellationToken)
                .ConfigureAwait(false);
            return Ok(new
            {
                Status = result,
                CheckedAt = DateTime.UtcNow,
            });
        }
    }
}
