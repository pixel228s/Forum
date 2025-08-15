using AutoMapper;
using Forum.Application.Common.Behaviors;
using Forum.Application.Common.Dtos.Posts.Requests;
using Forum.Application.Features.PostFeatures.Commands.ChangeState;
using Forum.Application.Features.PostFeatures.Commands.CreatePost;
using Forum.Application.Features.PostFeatures.Commands.DeletePost;
using Forum.Application.Features.PostFeatures.Commands.UpdatePost;
using Forum.Application.Features.PostFeatures.Queries.GetAllPosts;
using Forum.Application.Features.PostFeatures.Queries.RetrievePendingPosts;
using Forum.Application.Features.PostFeatures.Queries.RetrievePost;
using Forum.Application.Features.PostFeatures.Queries.RetrievePostComments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Forum.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public PostsController(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("create-post")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreatePostCommand>(request);
            command.userId = User.GetUserId();

            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPut("update-post/{postId}")]
        public async Task<IActionResult> UpdatePost(int postId, [FromBody] UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdatePostCommand>(request);
            command.Id = postId;
            command.UserId = User.GetUserId();

            var result = await _mediator.Send(command, cancellationToken)
                .ConfigureAwait(false);
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
            var query = new GetPostCommentsByIdQuery 
            {
                PostId = postId 
            };
            var result = await _mediator.Send(query, cancellationToken)
                .ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("/posts")]
        public async Task<IActionResult> GetAllPosts(CancellationToken cancellationToken)
        {
            var posts = await _mediator.Send(new GetAllPostsQuery(), 
                cancellationToken)
                .ConfigureAwait(false);
            return Ok(posts);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{postId}/change-state")]
        public async Task<IActionResult> ChangePostState(int postId, bool IsAccepted, CancellationToken cancellationToken)
        {
            var command = new ChangeStateCommand 
            {
                PostId = postId, 
                IsAccepted = IsAccepted 
            };
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(); 
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/pending-posts")]
        public async Task<IActionResult> GetPendingPosts(CancellationToken cancellationToken)
        {
            var list = await _mediator.Send(new GetPendingPostsQuery(), cancellationToken)
                .ConfigureAwait(false);
            return Ok(list);
        }
    }
}
