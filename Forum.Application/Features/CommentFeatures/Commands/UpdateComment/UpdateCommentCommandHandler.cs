using AutoMapper;
using Forum.Application.Common.Dtos.Comments.Responses;
using Forum.Application.Exceptions;
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
                .GetCommentById(request.CommentId, false, cancellationToken).ConfigureAwait(false);

            if (comment == null)
            {
                throw new ObjectNotFoundException();
            }

            if (comment.UserId != request.UserId)
            {
                throw new ActionForbiddenException();
            }

            comment.Text = request.Content ?? comment.Text;

            await _commentRepository.UpdateEntity(comment, cancellationToken);

            return _mapper.Map<CommentResponseDto>(comment);
        }
    }
}
