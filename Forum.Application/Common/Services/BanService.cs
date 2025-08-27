using Forum.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Forum.Application.Common.RevokeExpiredBans
{
    public class BanService : IBanService
    {
        private readonly IBanRepository _banRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionFactory _transactionFactory;
        private readonly ILogger<BanService> _logger;       
        public BanService(IBanRepository banRepository, 
            IUserRepository userRepository,
            ITransactionFactory transactionFactory,
            ILogger<BanService> logger)
        {
            _banRepository = banRepository;
            _userRepository = userRepository;
            _transactionFactory = transactionFactory;
            _logger = logger;
        }

        public async Task RevokeExpiredBans(CancellationToken cancellationToken)
        {
            using var transaction = await _transactionFactory.OpenTransactionAsync(cancellationToken)
                .ConfigureAwait(false);

            try
            {
                int updatedColumns = await _userRepository.UpdateBannedUsers(cancellationToken)
                    .ConfigureAwait(false);
                int removedColumns = await _banRepository.DeleteExpiredBans(cancellationToken)
                    .ConfigureAwait(false);

                _logger.Log(LogLevel.Information, $"Updated Columns {updatedColumns}");
                _logger.Log(LogLevel.Information, $"Updated Columns {removedColumns}");
                await transaction.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
            }
        }
    }
}
