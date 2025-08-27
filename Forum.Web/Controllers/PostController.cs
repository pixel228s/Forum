using Forum.Application.Common.Behaviors;
using Forum.Application.Features.CommentFeatures.Commands.CreateComment;
using Forum.Application.Features.CommentFeatures.Commands.DeleteComment;
using Forum.Application.Features.CommentFeatures.Commands.UpdateComment;
using Forum.Application.Features.PostFeatures.Commands.CreatePost;
using Forum.Application.Features.PostFeatures.Commands.DeletePost;
using Forum.Application.Features.PostFeatures.Commands.UpdatePost;
using Forum.Application.Features.PostFeatures.Queries.RetrievePost;
using Forum.Application.Features.PostFeatures.Queries.RetrievePostComments;
using Forum.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IMediator _mediator;
        private const int PageSize = 5;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Comments(int postId)
        {
            var post = await _mediator.Send(new GetPostByIdQuery { PostId = postId });

            var getCommentsQuery = new GetPostCommentsByIdQuery
            {
                PostId = postId,
            };

            var comments = await _mediator.Send(getCommentsQuery);

            var model = new CommentsViewModel
            {
                Post = post,
                Comments = comments
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(int postId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                TempData["Error"] = "Comment cannot be empty.";
                return RedirectToAction("Comments", "Post", new { postId });
            }

            var userId = User.GetUserId();

            var command = new CreateCommentCommand
            {
                Content = content,
                UserId = userId,
                PostId = postId
            };

            await _mediator.Send(command);

            return RedirectToAction("Comments", "Post", new { postId });
        }

        [HttpPost]
        public async Task<IActionResult> EditComment(string content, int commentId, int postId)
        {
            Console.WriteLine($"EditComment received: commentId={commentId}, postId={postId}, content={content}");
            var userId = User.GetUserId();

            var command = new UpdateCommentCommand
            {
                UserId = userId,
                Content = content,
                CommentId = commentId
            };

            await _mediator.Send(command);

            return RedirectToAction("Comments", "Post", new { postId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePost(string title, string content, int postId)
        {
            var command = new UpdatePostCommand
            {
                UserId = User.GetUserId(),
                Title = title,
                Content = content,
                Id = postId
            };

            await _mediator.Send(command);

            return RedirectToAction("Profile", "Profile");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var command = new DeletePostCommand
            {
                PostId = postId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
            };
            await _mediator.Send(command);
            return RedirectToAction("Profile", "Profile");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(string title, string content, IFormFile image)
        {
            var command = new CreatePostCommand
            {
                Content = content,
                Title = title,
                Image = image,
                userId = User.GetUserId()
            };

            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId, int postId)
        {
            var command = new DeleteCommentCommand
            {
                CommentId = commentId,
                UserId = User.GetUserId()
            };

            await _mediator.Send(command);
            return RedirectToAction("Comments", "Post", new { postId });
        }
    }
}
