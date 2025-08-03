using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Requests;
using Forum.Application.Features.PostFeatures.Commands.CreatePost;
using Forum.Application.Features.PostFeatures.Commands.UpdatePost;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PostsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("create-post")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("update-post")]
        public async Task<IActionResult> CreatePost(int postId, [FromBody] UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdatePostCommand>(request);
            command.Id = postId;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

    }
}
