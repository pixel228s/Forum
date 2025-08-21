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
        private readonly ITransactionFactory _transactionFactory;
        private readonly IDistributedCache _distributedCache;
        public DeleteBanCommandHandler(UserManager<User> userManager, 
            IBanRepository banRepository,
            ITransactionFactory transactionFactory,
            IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _banRepository = banRepository;
            _transactionFactory = transactionFactory;
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

            if (ban == null || ban.UserId.ToString() != request.UserId)
            {
                throw new ObjectNotFoundException();
            }

            user.IsBanned = false;

            using var transaction = await _transactionFactory.OpenTransactionAsync(cancellationToken)
               .ConfigureAwait(false);
            try
            {
                var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    string message = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new AppException(message);
                }

                await _banRepository.RemoveAsync(ban, cancellationToken).ConfigureAwait(false);

                string key = $"is-banned:{user.Id}";
                await _distributedCache.RemoveAsync(key).ConfigureAwait(false);

                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch(Exception)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                throw;
            }
            return Unit.Value;
        }
    }
}
