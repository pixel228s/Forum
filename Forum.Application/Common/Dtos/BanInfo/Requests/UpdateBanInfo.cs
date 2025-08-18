namespace Forum.Application.Common.Dtos.BanInfo.Requests
{
    public class UpdateBanInfo
    {
        public string BanReason { get; set; }
        public DateTime? BanEndDate { get; set; }
    }
}
