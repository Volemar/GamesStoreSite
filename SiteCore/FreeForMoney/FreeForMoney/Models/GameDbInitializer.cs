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
            Game game1 = new Game { 
                Name = "Fallout 4", 
                ReleaseDate = 2015, 
                Price = 40,
                Genre = "Action RPG",
                Description = "Game Description"
            };

            Game game2 = new Game
            {
                Name = "Skyrim Special Edition",
                ReleaseDate = 2016,
                Price = 60,
                Genre = "Action RPG",
                Description = "Game Description"
            };

            Game game3 = new Game
            {
                Name = "Tomb Raider",
                ReleaseDate = 2014,
                Price = 40,
                Genre = "Survival Shooter",
                Description = "Game Description"
            };

            Game game4 = new Game
            {
                Name = "Wolfenstein The New Order",
                ReleaseDate = 2016,
                Price = 30,
                Genre = "Shooter",
                Description = "Game Description"
            }; 
            
            Game game5 = new Game
            {
                Name = "Wolfenstein The New Order",
                ReleaseDate = 2016,
                Price = 30,
                Genre = "Shooter",
                Description = "Game Description"
            };
            Game game6 = new Game
            {
                Name = "Wolfenstein The New Order",
                ReleaseDate = 2016,
                Price = 30,
                Genre = "Shooter",
                Description = "Game Description"
            };
            Game game7 = new Game
            {
                Name = "Wolfenstein The New Order",
                ReleaseDate = 2016,
                Price = 30,
                Genre = "Shooter",
                Description = "Game Description"
            };
            Game game8 = new Game
            {
                Name = "Wolfenstein The New Order",
                ReleaseDate = 2016,
                Price = 30,
                Genre = "Shooter",
                Description = "Game Description"
            };
            db.Games.Add(game1);
            db.Games.Add(game2);
            db.Games.Add(game3);
            db.Games.Add(game4);
            db.Games.Add(game5);
            db.Games.Add(game6);
            db.Games.Add(game7);
            db.Games.Add(game8);
            Company company1 = new Company { Name = "Bethesda", Location = "Company location text", Director = "Someone" };
            company1.Games.Add(game1);
            company1.Games.Add(game2);
            company1.Games.Add(game3);
            Company company2 = new Company { Name = "Naughty Dog", Location = "Company location text", Director = "Someone" };
            company2.Games.Add(game4);
            company2.Games.Add(game5);
            company2.Games.Add(game6);
            company2.Games.Add(game7);
            company2.Games.Add(game8);
            db.Companies.Add(company1);
            db.Companies.Add(company2);
            base.Seed(db);
        }
    }
}

