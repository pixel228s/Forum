using AutoMapper;
using Forum.Application.Common.Dtos.BanInfo.Responses;
using Forum.Application.Exceptions;
using Forum.Domain.Interfaces;
using MediatR;

namespace Forum.Application.Features.AdminFeatures.Commands.UpdateBan
{
    public class UpdateBanCommandHandler : IRequestHandler<UpdateBanCommand, BanInfoResponse>
    {
        private readonly IBanRepository _banRepository;
        private readonly IMapper _mapper;
        public UpdateBanCommandHandler(IBanRepository banRepository, IMapper mapper)
        {
            _banRepository = banRepository;
            _mapper = mapper;
        }

        public async Task<BanInfoResponse> Handle(UpdateBanCommand request, CancellationToken cancellationToken)
        {
            var ban = await _banRepository.GetBanById(request.Id, cancellationToken)
                .ConfigureAwait(false);

            if (ban == null)
            {
                throw new ObjectNotFoundException();
            }

            ban.BanEndDate = request.BannedUntil ?? ban.BanEndDate;
            ban.BanReason = request.BanReason ?? ban.BanReason;

            await _banRepository.UpdateEntity(ban, cancellationToken).ConfigureAwait(false);
            return _mapper.Map<BanInfoResponse>(ban);
        }
    }
}
