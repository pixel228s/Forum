namespace Forum.Application.Common.RevokeExpiredBans
{
    public interface IBanService
    {
        Task RevokeExpiredBans(CancellationToken cancellationToken);
    }
}
