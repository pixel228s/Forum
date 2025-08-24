using Forum.Application.Features.PostFeatures.Queries.RetrievePost;
using Forum.Application.Features.PostFeatures.Queries.RetrievePostComments;
using Forum.Domain.Parameters;
using Forum.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IMediator _mediator;
        private const int PageSize = 5; // number of comments per page

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Comments(int postId, int pageNumber = 1)
        {
            var post = await _mediator.Send(new GetPostByIdQuery { PostId = postId });

            var parameters = new RequestParameters
            {
                PageNumber = pageNumber,
                PageSize = PageSize
            };
            var getCommentsQuery = new GetPostCommentsByIdQuery
            {
                PostId = postId,
                parameters = parameters
            };
            var pagedComments = await _mediator.Send(getCommentsQuery);

            var model = new CommentsViewModel
            {
                Post = post,
                Comments = pagedComments
            };

            ViewData["PageNumber"] = pageNumber;

            return View(model);
        }
    }
}
