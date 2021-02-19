using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Irisi_M_301072868.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Irisi_M_301072868.Controllers
{
    public class PlayersController : Controller
    {
        private IRepository repository;

        private readonly ApplicationDbContext _context;

        public PlayersController(IRepository repo, ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            repository = repo;
        }

        public ActionResult ManagePlayers()
        {
            List<Player> players = _context.Players.Include(x => x.clubs).ToList();
            return View(players);
        }

        public ActionResult Details(int id)
        {
            Player player = _context.Players.Include(x=>x.clubs).Where(x=>x.PlayerId==id).FirstOrDefault();
            return View(player);
        }

        public ActionResult Create()
        {
            ViewBag.clubsList = _context.Clubs.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Player model)
        {
            try
            { 
                if(model!=null)
                _context.Players.Add(model);
                _context.SaveChanges();

                return RedirectToAction(nameof(ManagePlayers));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Player player = _context.Players.Find(id);
            ViewBag.clubsList = _context.Clubs.ToList();
            return View(player);
        }

        [HttpPost]
        public ActionResult Edit(Player player)
        {
            try
            {
                var entity = _context.Players.Where(m => m.PlayerId == player.PlayerId).FirstOrDefault();

                entity.PlayerId = player.PlayerId;
                entity.FirstName = player.FirstName;
                entity.LastName = player.LastName;
                entity.PhoneNo = player.PhoneNo;
                entity.Email = player.Email;
                entity.DayOfBirth = player.DayOfBirth;
                entity.ClubId = player.ClubId;
                entity.ClubJoinedDate = player.ClubJoinedDate;
                entity.Gender = player.Gender;
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction(nameof(ManagePlayers));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var model = _context.Players.Find(id);
                _context.Players.Remove(model);
                _context.SaveChanges();

                return RedirectToAction(nameof(ManagePlayers));
            }
            catch
            {
                return View();
            }
        }
    }
}