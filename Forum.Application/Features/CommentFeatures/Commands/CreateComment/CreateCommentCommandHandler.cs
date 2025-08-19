using AutoMapper;
using Forum.Application.Common.Dtos.Comments.Responses;
using Forum.Application.Exceptions;
using Forum.Domain.Entities.Comments;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.CommentFeatures.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentResponseDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;   
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(
            ICommentRepository commentRepository,
            IPostRepository postRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<CommentResponseDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Comment>(request);

            var post = await _postRepository
                .GetPostByIdAsync(request.PostId, cancellationToken, false, false)
                .ConfigureAwait(false);

            if (post == null)
            {
                throw new ObjectNotFoundException("No such post found");
            }

            await _commentRepository.AddAsync(comment, cancellationToken).ConfigureAwait(false);

            return _mapper.Map<CommentResponseDto>(comment);
        }
    }
}
