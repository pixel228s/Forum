namespace Forum.Application.Common.Dtos.Comments.Requests
{
    public class CreateCommentRequest
    {
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}
