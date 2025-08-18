namespace Forum.Application.Common.Dtos.BanInfo.Requests
{
    public class CreateBanRequest
    {
        public int UserId { get; set; }
        public string BanReason { get; set; }
        public DateTime? BanEndDate { get; set; }
    }
}
