namespace Forum.Application.Common.Dtos.BanInfo.Responses
{
    public class BanInfoResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CreationDate { get; set; }
        public string BanReason { get; set; }
        public DateTime? BanEndDate { get; set; }
    }
}
