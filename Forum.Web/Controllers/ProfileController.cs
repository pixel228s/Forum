using Forum.Application.Common.Behaviors;
using Forum.Application.Features.UserFeatures.Commands.DeleteImage;
using Forum.Application.Features.UserFeatures.Commands.UploadProfilePicture;
using Forum.Application.Features.UserFeatures.Queries.GetUserPosts;
using Forum.Application.Features.UserFeatures.Queries.RetrieveUserById;
using Forum.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async  Task<IActionResult> Profile()
        {
            var UserID = User.GetUserId();
            var query = new GetUserByIdQuery(UserID);
            var userDetails = await _mediator.Send(query);

            var getUserPostsQuery = new GetUserPostsQuery(UserID);
            var userPosts = await _mediator.Send(getUserPostsQuery);

            var model = new ProfileViewModel
            {
                UserResponse = userDetails,
                UserPosts = userPosts
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(IFormFile ProfileImage)
        {
            if (ProfileImage == null || ProfileImage.Length == 0)
                return BadRequest("No image selected");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var commend = new UploadImageCommand
            {
                UserId = userId!,
                Image = ProfileImage
            };
            await _mediator.Send(commend);

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProfilePicture()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new DeleteImageCommand
            {
                UserId = userId!
            };
            await _mediator.Send(command);

            return RedirectToAction("Profile");
        }
    }
}
