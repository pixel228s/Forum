using AutoMapper;
using Forum.Application.Common.Behaviors;
using Forum.Application.Common.Dtos.Comments.Requests;
using Forum.Application.Features.CommentFeatures.Commands.CreateComment;
using Forum.Application.Features.CommentFeatures.Commands.DeleteComment;
using Forum.Application.Features.CommentFeatures.Commands.UpdateComment;
using Forum.Application.Features.CommentFeatures.Queries.GetComment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CommentsController(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id, CancellationToken cancellationToken)
        {
            var query = new GetCommentByIdQuery 
            {
                Id = id
            };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost("/create-comment")]
        public async Task<IActionResult> CreateComment(CreateCommentRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateCommentCommand>(request);
            command.UserId = User.GetUserId();
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPatch("/{commentId}/update-comment")]
        public async Task<IActionResult> CreateComment(int commentId, UpdateCommentRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateCommentCommand>(request);

            command.CommentId = commentId;
            command.UserId = User.GetUserId();

            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpDelete("/{commentId}/delete-comment")]
        public async Task<IActionResult> DeleteComment(int commentId, CancellationToken cancellationToken)
        {
            var comment = new DeleteCommentCommand
            {
                CommentId = commentId,
                UserId = User.GetUserId()
            };
            await _mediator.Send(comment, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }
    }
}
