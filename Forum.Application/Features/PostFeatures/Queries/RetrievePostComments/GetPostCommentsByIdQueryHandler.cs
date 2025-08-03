using AutoMapper;
using Forum.Application.Common.Dtos.Comments.Responses;
using Forum.Application.Exceptions;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.PostFeatures.Queries.RetrievePostComments
{
    public class GetPostCommentsByIdQueryHandler : IRequestHandler<GetPostCommentsByIdQuery, IEnumerable<CommentResponseDto>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public GetPostCommentsByIdQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentResponseDto>> Handle(GetPostCommentsByIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository
                .GetCommentsByPostId(request.PostId, true, cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<CommentResponseDto>>(comments);
        }
    }
}
