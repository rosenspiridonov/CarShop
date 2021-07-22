namespace MobileBgDataScraper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using MobileBgDataScraper.Models;

    using AngleSharp;
    using AngleSharp.Dom;
    using CarShop.Data;
    using MobileBgDataScraper.Seeders;

    class Program
    {
        private static IConfiguration config;
        private static IBrowsingContext context;

        static void Main(string[] args)
        {
            config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);

            var carsService = new CarService(config, context);

            var cars = carsService.PopulateCars(1, 2);

            var carsSeeder = new CarsSeeder(new ApplicationDbContext());

            foreach (var car in cars)
            {
                carsSeeder.SeedCar(car);
            }
           
        }

        
    }
}
