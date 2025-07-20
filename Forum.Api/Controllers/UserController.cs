using Forum.Application.Features.UserFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(user);
        }
    }
}
