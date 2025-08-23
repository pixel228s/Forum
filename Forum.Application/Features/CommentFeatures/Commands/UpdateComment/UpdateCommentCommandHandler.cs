using AutoMapper;
using Forum.Application.Common.Dtos.Comments.Responses;
using Forum.Application.Exceptions;
using Forum.Application.Exceptions.Models;
using Forum.Domain.Entities.Posts.Enums;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.CommentFeatures.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, CommentResponseDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public UpdateCommentCommandHandler(
            ICommentRepository commentRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<CommentResponseDto> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository
                .GetCommentById(request.CommentId, false, true, cancellationToken).ConfigureAwait(false);

            if (comment == null)
            {
                throw new ObjectNotFoundException("Comment with this id could not be found");
            }

            if (comment.UserId != request.UserId)
            {
                throw new ActionForbiddenException("You can't commit this action");
            }

            if (comment.Post.Status == Status.Inactive)
            {
                throw new AppException("Can not update comment, the post is inactive");
            }

            comment.Text = request.Content ?? comment.Text;

            await _commentRepository.UpdateEntity(comment, cancellationToken).ConfigureAwait(false);

            return _mapper.Map<CommentResponseDto>(comment);
        }
    }
}
