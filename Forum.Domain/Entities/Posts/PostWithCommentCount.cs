using Forum.Domain.Models.Posts;

namespace Forum.Domain.Entities.Posts
{
    public class PostWithCommentCount
    {
        public Post post { get; set; }
        public int commentCount { get; set; }
        public string authorUsername { get; set; }
        public string? authorPfp { get; set; }
    }
}
