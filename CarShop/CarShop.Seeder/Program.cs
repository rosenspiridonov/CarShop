namespace CarShop.Seeder
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;

    using CarShop.Data;
    using MobileBgDataScraper;
    using System;

    class Program
    {
        private static ApplicationDbContext db;

        static void Main(string[] args)
        {
            db = new ApplicationDbContext();

            SeedCars(1, 10);
            SeedRoles();
        }

        private static void SeedCars(int startPage, int endPage)
        {
            if (db.Cars.Any())
            {
                return;
            }

            var carsService = new CarService();

            var cars = carsService.PopulateCars(startPage, endPage);

            var carsSeeder = new CarsSeeder(db);

            foreach (var car in cars)
            {
                try
                {
                    carsSeeder.SeedCar(car);
                }
                catch (Exception)
                { }
            }
        }

        private static void SeedRoles()
        {
            if (db.Roles.Any())
            {
                return;
            }

            db.Roles.AddRange(new List<IdentityRole>()
            {
                new IdentityRole() { Name = "User" },
                new IdentityRole() { Name = "Dealer" },
                new IdentityRole() { Name = "Admin" },
            });

            db.SaveChanges();
        }
    }
}
