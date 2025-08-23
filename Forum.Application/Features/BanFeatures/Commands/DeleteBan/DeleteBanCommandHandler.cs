using Forum.Application.Exceptions;
using Forum.Application.Exceptions.Models;
using Forum.Domain.Interfaces;
using Forum.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;

namespace Forum.Application.Features.BanFeatures.Commands.UnbanUser
{
    public class DeleteBanCommandHandler : IRequestHandler<DeleteBanCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IBanRepository _banRepository;
        private readonly IDistributedCache _distributedCache;
        public DeleteBanCommandHandler(UserManager<User> userManager, 
            IBanRepository banRepository,
            IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _banRepository = banRepository;
            _distributedCache = distributedCache;
        }

        public async Task<Unit> Handle(DeleteBanCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new ObjectNotFoundException();
            }

            var ban = await _banRepository.GetBanById(request.BanId, cancellationToken);

            if (ban == null || ban.UserId != user.Id)
            {
                throw new ObjectNotFoundException();
            }
            await _banRepository.RemoveAsync(ban, cancellationToken).ConfigureAwait(false);

            string key = $"is-banned:{user.Id}";
            await _distributedCache.RemoveAsync(key).ConfigureAwait(false);
       
            return Unit.Value;
        }
    }
}
