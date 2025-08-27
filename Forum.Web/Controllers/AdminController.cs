using Forum.Application.Features.AdminFeatures.Commands.BanUser;
using Forum.Application.Features.AdminFeatures.Commands.UpdateBan;
using Forum.Application.Features.AdminFeatures.Queries.GetAllBans;
using Forum.Application.Features.BanFeatures.Commands.UnbanUser;
using Forum.Application.Features.PostFeatures.Commands.ChangeState;
using Forum.Application.Features.PostFeatures.Queries.RetrievePendingPosts;
using Forum.Application.Features.UserFeatures.Queries.GetAllUsers;
using Forum.Application.Features.UserFeatures.Queries.UserCount;
using Forum.Domain.Parameters;
using Forum.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> AllUsers(int page = 1)
        {
            int pageSize = 5;
            var requestParameters = new RequestParameters
            {
                PageNumber = page,
                PageSize = pageSize
            };

            var users = await _mediator.Send(new GetAllUsersQuery { parameters = requestParameters });

            var totalUsers = await _mediator.Send(new GetUserCountQuery());

            var viewModel = new AllUsersViewModel
            {
                Responses = users,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalUsers / (double)pageSize)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBan(string banReason, DateTime banEndDate, int userId)
        {
            var command = new BanUserCommand 
            {
                BanReason = banReason,
                BanEndDate = banEndDate,
                UserId = userId
            };
            var info = await _mediator.Send(command);
            return RedirectToAction("AllUsers");
        }

        [HttpGet]
        public async Task<IActionResult> PendingPosts(int page = 1)
        {
            int pageSize = 5;
            var requestParameters = new RequestParameters
            {
                PageNumber = page,
                PageSize = pageSize
            };

            var posts = await _mediator.Send(new GetPendingPostsQuery (requestParameters));

            var viewModel = new PendingPostsViewModel
            {
                PostResponses = posts,
                CurrentPage = page,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ApprovePost(int postId)
        {
            Console.WriteLine(postId);
            var command = new ChangeStateCommand { PostId = postId, IsAccepted = true };
            await _mediator.Send(command);

            return RedirectToAction("PendingPosts");
        }

        [HttpPost]
        public async Task<IActionResult> RejectPost(int postId)
        {
            var command = new ChangeStateCommand { PostId = postId, IsAccepted = false };
            await _mediator.Send(command);

            return RedirectToAction("PendingPosts");
        }

        [HttpGet]
        public async Task<IActionResult> Bans(int page = 1)
        {
            int pageSize = 5;
            var requestParameters = new RequestParameters
            {
                PageNumber = page,
                PageSize = pageSize
            };

            var posts = await _mediator.Send(new GetAllBansQuery(requestParameters));

            var viewModel = new BansViewModel
            {
                Bans = posts,
                CurrentPage = page
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBan(int id, int userId, string reason, DateTime endDate)
        {
            var command = new UpdateBanCommand
            {
                UserId = userId,
                Id = id,
                BanReason = reason,
                BannedUntil = endDate
            };
            await _mediator.Send(command);

            return RedirectToAction("Bans");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBan(int id, int banId)
        {
            Console.WriteLine(id);
            Console.WriteLine(banId);
            var command = new DeleteBanCommand
            {
                UserId = id.ToString(),
                BanId = banId
            };
            await _mediator.Send(command);

            return RedirectToAction("Bans");
        }
    }
}
