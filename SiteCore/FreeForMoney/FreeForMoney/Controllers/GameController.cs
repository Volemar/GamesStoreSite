using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreeForMoney.Models;

namespace FreeForMoney.Controllers
{
    public class GameController : Controller
    {
        private GameContext gameContext = new GameContext();
        private PurchaseContext purchaseContext = new PurchaseContext();

        // GET: Players
        public ActionResult Index()
        {
            var games = gameContext.Games.Include(p => p.Company);
            return View(games.ToList());
        }
        public ActionResult UploadImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = gameContext.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }
        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = gameContext.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(gameContext.Companies, "Id", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ReleaseDate,Genre,Description,Price,CompanyId")] Game game)
        {
            if (ModelState.IsValid)
            {
                gameContext.Games.Add(game);
                gameContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamId = new SelectList(gameContext.Companies, "Id", "Name", game.CompanyId);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = gameContext.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamId = new SelectList(gameContext.Companies, "Id", "Name", game.CompanyId);
            return View(game);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ReleaseDate,Genre,Description,Price,CompanyId")] Game game)
        {
            if (ModelState.IsValid)
            {
                gameContext.Entry(game).State = EntityState.Modified;
                gameContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamId = new SelectList(gameContext.Companies, "Id", "Name", game.CompanyId);
            return View(game);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = gameContext.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = gameContext.Games.Find(id);
            gameContext.Games.Remove(game);
            gameContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                gameContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}