namespace Forum.Domain.Entities.Comments
{
    public class CommentWithUserInfo
    {
        public Comment Comment { get; set; }
        public string UserName { get; set; }
        public string? UserProfilePicUrl { get; set; }
    }
}
