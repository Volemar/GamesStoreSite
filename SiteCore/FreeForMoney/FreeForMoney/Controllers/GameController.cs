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
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.GenreSortParm = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            var games = from s in gameContext.Games.Include(p => p.Company)
                    select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.Name.Contains(searchString)
                                       || s.Genre.Contains(searchString)
                                       || s.Company.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    games = games.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    games = games.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    games = games.OrderByDescending(s => s.ReleaseDate);
                    break;
                case "Genre":
                    games = games.OrderBy(s => s.Genre);
                    break;
                case "genre_desc":
                    games = games.OrderByDescending(s => s.Genre);
                    break;
                case "Price":
                    games = games.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    games = games.OrderByDescending(s => s.Price);
                    break;
                default:
                    games = games.OrderBy(s => s.Name);
                    break;
            }
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