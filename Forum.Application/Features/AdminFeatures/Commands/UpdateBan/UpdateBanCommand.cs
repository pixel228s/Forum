namespace Forum.Application.Features.AdminFeatures.Commands.UpdateBan
{
    public class UpdateBanCommand
    {
        public int Id { get; set; }
        public string? BanReason { get; set; }
        public DateTime BannedUntil { get; set; }
    }
}
