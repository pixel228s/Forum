using Forum.Application.Features.PostFeatures.Queries.GetAllPosts;
using Forum.Domain.Parameters;
using Forum.Models;
using Forum.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
