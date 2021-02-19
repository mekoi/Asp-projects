using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Irisi_M_301072868.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult AddClub()
        {
            return View();
        }

        public ViewResult ClubDetailsEmpty()
        {
            return View();
        }

        public ViewResult ManagePlayers()
        {
            return View();
        }
    }
}