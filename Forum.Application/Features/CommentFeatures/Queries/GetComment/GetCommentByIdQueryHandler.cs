using AutoMapper;
using Forum.Application.Common.Dtos.Comments.Responses;
using Forum.Application.Exceptions;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.CommentFeatures.Queries.GetComment
{
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentResponseDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentByIdQueryHandler(ICommentRepository commentRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<CommentResponseDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetCommentById(request.Id, true, false, cancellationToken)
                .ConfigureAwait(false); 

            if (comment == null)
            {
                throw new ObjectNotFoundException();
            }

            return _mapper.Map<CommentResponseDto>(comment);
        }
    }
}
