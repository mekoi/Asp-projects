using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Irisi_Bruno_lab3.Models;
using Irisi_Bruno_lab3.Models.ViewModels;

namespace Irisi_Bruno_lab3.Controllers
{
    public class UsersController : Controller
    {
        private readonly MoviesReviewsContext _context;
        public static Users userActiveSession;

        public UsersController(MoviesReviewsContext context)
        {
            _context = context;
        }

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserViewModel creatingUser)
        {
            Users newUser = new Users();
            if (ModelState.IsValid)
            {
                Users existingUser = _context.Users.FirstOrDefault(p => p.UserName == creatingUser.UserName);
                if (existingUser == null)
                {
                    newUser.UserName = creatingUser.UserName;
                    newUser.UserPassword = creatingUser.UserPassword;
                    SaveUserInDatabase(newUser);
                    userActiveSession = newUser;
                    TempData["UserCreatedSuccessfullyMessage"] = "User created successfully"; 
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    TempData["UserCreatedFailedMessage"] = "This username exists in database. Try to create another username";
                    return RedirectToAction("Register", "Users");
                }
            }
            return View("Index");
        }

        public void SaveUserInDatabase(Users newUser)
        {        
            Users dbEntry = _context.Users.FirstOrDefault(p => p.UserName == newUser.UserName);
            if (dbEntry == null)
            {
                _context.Users.Add(newUser);
            }  
            _context.SaveChanges();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user)
        {
            if (ModelState.IsValid)
            {
                var existingDbUser = _context.Users.Where(a => a.UserName.Equals(user.UserName) && a.UserPassword.Equals(user.UserPassword)).FirstOrDefault();
                if (existingDbUser != null)
                {
                    TempData["UserLoggedInAs"] = existingDbUser.UserName;
                    userActiveSession = existingDbUser;

                    return Redirect("~/");
                    //RedirectToAction("Index", "Home");
                    //return View("../Home/Index");
                }
                else
                {
                    ViewBag.FailedLogin = "UserName or Password do not exit. Please enter correct credentials.";
                    ModelState.Clear();
                    return View();
                }
            }
            else
            {
                ViewBag.InvalidUser = "InvalidUserObject";
               
            }

            return View("../Home/Index");

        }

        // GET: Login
        public IActionResult Login()
        {       
            if (TempData["UserCreatedSuccessfullyMessage"] != null)
            { 
                ViewBag.UserCreatedSuccessfullyMessage = TempData["UserCreatedSuccessfullyMessage"].ToString();
            }
            return View();
        }
       
        public ActionResult Logout()
        {
            userActiveSession = null;
            return View("../Home/Index", userActiveSession);
        }
    }
}
