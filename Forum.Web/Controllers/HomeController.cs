using Forum.Application.Features.PostFeatures.Queries.GetAllPosts;
using Forum.Domain.Parameters;
using Forum.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator, ILogger<HomeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            int pageSize = 5;
            var parameters = new RequestParameters
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var posts = await _mediator.Send(new GetAllPostsQuery(parameters));

            var model = new HomeViewModel
            {
                Posts = posts
            };

            ViewData["PageNumber"] = pageNumber;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Banned()
        {
            var message = HttpContext.Session.GetString("BanMessage") ?? "Your account has been suspended.";
            ViewBag.BanMessage = message;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorBytes = HttpContext.Session.Get("ErrorMessage");
            var model = new ErrorViewModel
            {
                ErrorMessage = errorBytes != null ? Encoding.UTF8.GetString(errorBytes) : "unhandled exception happened"
            };

            return View(model);
        }
    }
}
