using Forum.Application.Features.AccountFeatures.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginQuery loginQuery, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(loginQuery).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
