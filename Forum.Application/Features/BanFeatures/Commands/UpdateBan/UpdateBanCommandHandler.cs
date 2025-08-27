using AutoMapper;
using Forum.Application.Common.Dtos.BanInfo.Responses;
using Forum.Application.Exceptions;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Runtime.CompilerServices;

namespace Forum.Application.Features.AdminFeatures.Commands.UpdateBan
{
    public class UpdateBanCommandHandler : IRequestHandler<UpdateBanCommand, BanInfoResponse>
    {
        private readonly IBanRepository _banRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        public UpdateBanCommandHandler(IBanRepository banRepository, 
            IMapper mapper, 
            IDistributedCache distributedCache)
        {
            _banRepository = banRepository;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<BanInfoResponse> Handle(UpdateBanCommand request, CancellationToken cancellationToken)
        {
            var ban = await _banRepository.GetBanById(request.Id, cancellationToken)
                .ConfigureAwait(false);

            if (ban == null || ban.UserId != request.UserId)
            {
                throw new ObjectNotFoundException();
            }
          
            if (request.BannedUntil != null)
            {
                string key = $"is-banned:{request.UserId}";
                var ttl = request.BannedUntil - DateTime.Now;
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = ttl
                };
                await _distributedCache.SetStringAsync(key, "true", options, cancellationToken)
                    .ConfigureAwait(false);
                ban.BanEndDate = request.BannedUntil;
            }

            ban.BanReason = request.BanReason ?? ban.BanReason;

            await _banRepository.UpdateEntity(ban, cancellationToken).ConfigureAwait(false);
            return _mapper.Map<BanInfoResponse>(ban);
        }
    }
}
