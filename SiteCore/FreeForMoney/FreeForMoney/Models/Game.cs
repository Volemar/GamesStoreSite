using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FreeForMoney.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReleaseDate { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
    }
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string Location { get; set; }
        public ICollection<Game> Games { get; set; }
        public Company()
        {
            Games = new List<Game>();
        }
    }

    public class Purchase
    {
        public int PurchaseId { get; set; }
        public string Person { get; set; }
        public string Address { get; set; }
        public int GameId { get; set; }
        public DateTime Date { get; set; }
    }

    public class GameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
    public class PurchaseContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}