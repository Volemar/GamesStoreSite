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
                Name = "Wolfenstein: The New Order", 
                ReleaseDate = 2014, 
                Price = 40,
                Genre = "Action",
                Description = "Wolfenstein®: The New Order reignites the series that created the first-person shooter genre."
            };

            Game game2 = new Game
            {
                Name = "Skyrim Special Edition",
                ReleaseDate = 2016,
                Price = 60,
                Genre = "Action RPG",
                Description = "Winner of more than 200 Game of the Year Awards, Skyrim Special Edition brings the epic fantasy to life in stunning detail."
            };

            Game game3 = new Game
            {
                Name = "Tomb Raider",
                ReleaseDate = 2014,
                Price = 40,
                Genre = "Survival Shooter",
                Description = "Tomb Raider explores the intense origin story of Lara Croft and her ascent from a young woman to a hardened survivor."
            };

            Game game4 = new Game
            {
                Name = "Devil May Cry",
                ReleaseDate = 2008,
                Price = 30,
                Genre = "Shooter",
                Description = "The ultimate Devil Hunter is back in style, in the game action fans have been waiting for. Play for fun!"
            }; 
            
            Game game5 = new Game
            {
                Name = "The Witcher: Wild Hunt ",
                ReleaseDate = 2016,
                Price = 60,
                Genre = "Shooter",
                Description = "As war rages on throughout the Northern Realms, you take on the greatest contract of your life."
            };
            Game game6 = new Game
            {
                Name = "Grand Theft Auto 5",
                ReleaseDate = 2016,
                Price = 20,
                Genre = "Shooter",
                Description = "Grand Theft Auto V for PC offers players the option to explore the award-winning world of Los Santos and Blaine County."
            };
            Game game7 = new Game
            {
                Name = "Assassin`s Creed: Valhalla",
                ReleaseDate = 2016,
                Price = 30,
                Genre = "Shooter",
                Description = "Assassin's Creed Valhalla is a 2020 action role-playing video game developed by Ubisoft Montreal and published by Ubisoft."
            };
            Game game8 = new Game
            {
                Name = "Minecraft",
                ReleaseDate = 2016,
                Price = 15,
                Genre = "Shooter",
                Description = "Explore new gaming adventures, accessories, & merchandise on the Minecraft Official Site."
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

