namespace CarShop.Seeder
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;

    using CarShop.Data;
    using MobileBgDataScraper;
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    class Program
    {
        private static ApplicationDbContext db;

        static async Task Main(string[] args)
        {
            db = new ApplicationDbContext();

            await SeedRoles();
            SeedCars(1, 3);
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

        private static async Task SeedRoles()
        {
            if (db.Roles.Any())
            {
                return;
            }

            await db.Roles.AddRangeAsync(new List<IdentityRole>()
            {
                new IdentityRole() { Name = "User" },
                new IdentityRole() { Name = "Dealer" },
                new IdentityRole() { Name = "Admin" },
            });

            await db.SaveChangesAsync();
        }
    }
}
