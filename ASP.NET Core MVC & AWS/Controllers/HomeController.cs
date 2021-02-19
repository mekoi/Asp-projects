using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Irisi_Bruno_lab3.Models;

namespace Irisi_Bruno_lab3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MoviesReviewsContext _context;

        public HomeController(ILogger<HomeController> logger, MoviesReviewsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var moviesList = _context.Movie.Where(p => p.MovieId > 0);
            ViewBag.Movies = moviesList;
            ViewBag.UserLoggedInAs=TempData["UserLoggedInAs"];
            ViewBag.userActiveSession = UsersController.userActiveSession;
            return View(); 
        }
           
        // GET: Register
        public IActionResult Upload()
        {
            return View();
        }


    }
}
