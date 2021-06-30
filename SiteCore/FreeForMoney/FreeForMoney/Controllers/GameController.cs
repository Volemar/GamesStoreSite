using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using FreeForMoney.Models;
using PagedList;

namespace FreeForMoney.Controllers
{
    public class GameController : Controller
    {
        private GameContext gameContext = new GameContext();
        private PurchaseContext purchaseContext = new PurchaseContext();

        // GET: Players
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.GenreSortParm = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

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
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(games.ToPagedList(pageNumber, pageSize));
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
            ViewBag.CompanyId = new SelectList(gameContext.Companies, "Id", "Name");
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

            ViewBag.CompanyId = new SelectList(gameContext.Companies, "Id", "Name", game.CompanyId);
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

        public ActionResult SendEmail(int? id)
        {
            Game game = gameContext.Games.Find(id);
            return View(game);
        }
        [HttpPost]
        public ActionResult SendEmail(string receiver, Game game, string message)
        {
            try
            {
                Game currentGame = gameContext.Games.Find(game.Id);
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("FreeforMoneyShop@gmail.com", "FreeForMoney");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Freeformoney111";
                    var sub = "You bought a game!";
                    var body = "Hey, " + receiverEmail.User + " you just bought a " + currentGame.Name + " from Free For Money site!" +
                        " you owe us " + currentGame.Price + "$, so please, pay them as soon as possible! Bonus suvenires will be delivered to " + message + ".";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
    }

    
}