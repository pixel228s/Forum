using Forum.Application.Common.Dtos.BanInfo.Responses;

namespace Forum.Web.Models
{
    public class BansViewModel
    {
        public IEnumerable<BanInfoResponse> Bans { get; set; }
        public int CurrentPage { get; set; }
    }
}
