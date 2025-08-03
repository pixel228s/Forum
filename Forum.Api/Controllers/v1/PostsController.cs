using AutoMapper;
using Forum.Application.Common.Dtos.Posts.Requests;
using Forum.Application.Features.PostFeatures.Commands.ChangeState;
using Forum.Application.Features.PostFeatures.Commands.CreatePost;
using Forum.Application.Features.PostFeatures.Commands.DeletePost;
using Forum.Application.Features.PostFeatures.Commands.UpdatePost;
using Forum.Application.Features.PostFeatures.Queries.RetrievePost;
using Forum.Application.Features.PostFeatures.Queries.RetrievePostComments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPut("update-post/{postId}")]
        public async Task<IActionResult> CreatePost(int postId, [FromBody] UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdatePostCommand>(request);
            command.Id = postId;
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        [SwaggerResponse(204, "Post deleted successfully")]
        [HttpPut("delete-post/{postId}")]
        public async Task<IActionResult> DeletePost(int postId, CancellationToken cancellationToken)
        {
            var command = new DeletePostCommand { PostId = postId };
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPostById(int postId, CancellationToken cancellationToken)
        {
            var query = new GetPostByIdQuery { PostId = postId };
            var result = await _mediator.Send(query, cancellationToken)
                .ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{postId}/comments")]
        public async Task<IActionResult> GetCommentsByPostId(int postId, CancellationToken cancellationToken)
        {
            var query = new GetPostCommentsByIdQuery { PostId = postId };
            var result = await _mediator.Send(query, cancellationToken)
                .ConfigureAwait(false);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{postId}/change-state")]
        public async Task<IActionResult> ChangePostState(int postId, bool IsAccepted, CancellationToken cancellationToken)
        {
            var command = new ChangeStateCommand { PostId = postId, IsAccepted = IsAccepted };
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(); 
        }
    }
}
