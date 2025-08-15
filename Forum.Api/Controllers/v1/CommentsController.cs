using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        public CommentsController()
        {
        }

        [HttpPost("/create-comment")]
        public async Task<IActionResult> CreateComment(CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
