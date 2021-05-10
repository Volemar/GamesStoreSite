using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FreeForMoney.Models
{
    public class GameDbInitializer : DropCreateDatabaseAlways<GameContext>
    {
        protected override void Seed(GameContext db)
        {
            Game game1 = new Game { Name = "something1", ReleaseDate = 2003, Price = 60, Description = "Big Black ShipPovar" };
            Game game2 = new Game { Name = "something2", ReleaseDate = 2001, Price = 465, Description = "Big Red ShipPovar" };
            Game game3 = new Game { Name = "something3", ReleaseDate = 2201, Price = 45, Description = "Big White ShipPovar" };
            Game game4 = new Game { Name = "something4", ReleaseDate = 2031, Price = 54, Description = "Big Blue ShipPovar" };
            db.Games.Add(game1);
            db.Games.Add(game2);
            db.Games.Add(game3);
            db.Games.Add(game4);
            Company company1 = new Company { Name = "Company", Location = "Your mom", Director = "Me" };
            company1.Games.Add(game1);
            company1.Games.Add(game2);
            Company company2 = new Company { Name = "CompanyButBigger", Location = "Your father", Director = "Me" };
            company2.Games.Add(game3);
            company2.Games.Add(game4);
            db.Companies.Add(company1);
            db.Companies.Add(company2);
            base.Seed(db);
        }
    }
}

