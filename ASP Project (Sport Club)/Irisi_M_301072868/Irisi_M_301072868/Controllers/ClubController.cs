using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Irisi_M_301072868.Models;
using Irisi_M_301072868.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Irisi_M_301072868.Controllers
{
    public class ClubController : Controller
    {
        private IRepository repository;

        private readonly ApplicationDbContext _context;

        public ClubController(IRepository repo, ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            repository = repo;
        }

        public ViewResult AvailableClubs()
        {
            var clubsList = _context.Clubs.ToList();
            if (clubsList != null)
            {
                return View(clubsList);
            }
            return View(repository.Clubs);
        }

        [HttpGet]
        public ViewResult AddClub()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClub(Club club)
        {
            try
            {
                if (club != null)
                {
                    _context.Clubs.Add(club);
                    _context.SaveChanges();
                    return RedirectToAction("AvailableClubs");
                }
                return RedirectToAction("AvailableClubs");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpGet]
        public ViewResult ViewClubDetail(int id)
        {
            Club model = _context.Clubs.Find(id);
            return View("ClubDetails", model);
        }
        
        [HttpGet]
        public ViewResult EditClub(int id)
        {
            Club model = _context.Clubs.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditClub(Club club)
        {
            var clubEntity = _context.Clubs.Where(m => m.ClubId == club.ClubId).FirstOrDefault();

            clubEntity.ClubId = club.ClubId;
            clubEntity.ClubName = club.ClubName;
            clubEntity.AgeRange = club.AgeRange;
            clubEntity.CreatedDate = club.CreatedDate;
            _context.Entry(clubEntity).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("AvailableClubs");
        }
        
        public ActionResult DeleteClub(int id)
        {
            Club model = _context.Clubs.Find(id);
            _context.Clubs.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("AvailableClubs");
        }
    }
}