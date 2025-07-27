using Forum.Application.Features.UserFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Forum.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("user/{id}")]
        [SwaggerResponse(200, "User found successfully")]
        [SwaggerResponse(404, "User not found")]
        [SwaggerResponse(401, "Action not authorized")]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(user);
        }

        [HttpGet]
        [SwaggerResponse(200, "User found successfully")]
        [SwaggerResponse(404, "User not found")]
        [SwaggerResponse(401, "Action not authorized")]
        public async Task<IActionResult> GetUserById([FromQuery] string email, CancellationToken cancellationToken)
        {
            var query = new GetUserByEmailQuery(email);
            var user = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(user);
        }
    }
}
