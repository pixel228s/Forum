using AutoMapper;
using Forum.Application.Common.Dtos.Users.Requests;
using Forum.Application.Features.UserFeatures.Commands.DeleteUser;
using Forum.Application.Features.UserFeatures.Commands.UpdateUser;
using Forum.Application.Features.UserFeatures.Queries.GetUserPosts;
using Forum.Application.Features.UserFeatures.Queries.RetrieveUserByEmail;
using Forum.Application.Features.UserFeatures.Queries.RetrieveUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Forum.Api.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    //[ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, 
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "User found successfully")]
        [SwaggerResponse(404, "User not found")]
        [SwaggerResponse(401, "Action not authorized")]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(user);
        }

        [HttpGet()]
        [SwaggerResponse(200, "User found successfully")]
        [SwaggerResponse(404, "User not found")]
        [SwaggerResponse(401, "Action not authorized")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email, CancellationToken cancellationToken)
        {
            var query = new GetUserByEmailQuery(email);
            var user = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(user);
        }

        [HttpGet("{id}/posts")]
        [SwaggerResponse(200, "Posts retrieved")]
        [SwaggerResponse(401, "Action not authorized")]
        public async Task<IActionResult> GetUserPosts(int id, CancellationToken cancellationToken)
        {
            var query = new GetUserPostsQuery(id);
            var posts = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(posts);
        }

        [HttpGet("{id}/comments")]
        [SwaggerResponse(200, "Comments retrieved")]
        [SwaggerResponse(401, "Action not authorized")]
        public async Task<IActionResult> GetUserComments(int id, CancellationToken cancellationToken)
        {
            var query = new GetUserPostsQuery(id);
            var posts = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(posts);
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateUserCommand>(request);
            command.Id = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteUserCommand
            {
                UserId = userId,
                RequesterId = User.FindFirstValue(ClaimTypes.NameIdentifier)!
            });
            return NoContent();
        }
    }
}
