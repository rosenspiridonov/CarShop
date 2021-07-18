namespace MobileBgDataScraper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using AngleSharp;
    using AngleSharp.Dom;
    using MobileBgDataScraper.Models;

    class Program
    {
        private static IConfiguration config;
        private static IBrowsingContext context;

        static void Main(string[] args)
        {
            config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);

            var car = GetCar("https://www.mobile.bg/pcgi/mobile.cgi?act=4&adv=11626433709764309&slink=kt4wy4");
            var car2 = GetCar("https://www.mobile.bg/pcgi/mobile.cgi?act=4&adv=11620798005281686");
            var car3 = GetCar("https://www.mobile.bg/pcgi/mobile.cgi?act=4&adv=21626358782196243");
            var car4 = GetCar("https://www.mobile.bg/pcgi/mobile.cgi?act=4&adv=11625407559751380");

            return;
            
            // Iterate through pages with posts
            Parallel.For(1, 10, (i) => {
                var url = $"https://www.mobile.bg/pcgi/mobile.cgi?act=3&slink=kt4wy4&f1={i}";
                Console.WriteLine(url);
                var postUrls = GetPostUrls(url);

                // Iterate through all post on the page
                Parallel.ForEach(postUrls, (post) =>
                {
                    var car = GetCar(post);
                });
            });
        }

        private static CarDto GetCar(string url)
        {
            var document = context.OpenAsync(url)
                .GetAwaiter()
                .GetResult();

            var car = new CarDto();

            // Get Model, Brand and Modification
            var (model, brand, modification) = GetModelBrandAndModification(document);
            car.Model = model;
            car.Brand = brand;
            car.Modification = modification;

            car.Price = GetPrice(document);
            car.Description = GetDescription(document);

            car.ProduceYear = GetYear(document);
            car.EngineType = GetEngineType(document);

            return null;
        }

        private static IEnumerable<string> GetPostUrls(string url)
        {
            var document = context.OpenAsync(url)
                .GetAwaiter()
                .GetResult();

            var postsUrls = document.QuerySelectorAll(".photoLink")
                .Select(x => x.GetAttribute("href"))
                .Select(x => x = "https:" + x)
                .ToArray();

            return postsUrls;
        }

        private static (string model, string brand, string modification) GetModelBrandAndModification(IDocument document)
        {
            var html = document.DocumentElement.OuterHtml;
            var titleLine = document.QuerySelector("h1").TextContent;

            var brandRegex = new Regex(@"'AdvertBrand':\s?\['([A-z\s]+)'\]");
            var modelRegex = new Regex(@"'AdvertModel':\s?\['([A-z0-9]+)'\]");

            var brandMatch = brandRegex.Match(html);
            var modelMatch = modelRegex.Match(html);

            string brand = brandMatch.Groups[1].ToString();
            string model = modelMatch.Groups[1].ToString();
            string modification = titleLine
                .Replace(brand, "")
                .Replace(model, "")
                .Trim();

            if (modification == "")
            {
                modification = null;
            }

            return (brand, model, modification);
        }

        private static decimal GetPrice(IDocument document)
        {
            var priceAsString = document.QuerySelector("#details_price")
                .TextContent;

            priceAsString = Regex.Match(priceAsString, @"[0-9\s]+")
                .Value
                .Replace(" ", "");

            var price = decimal.Parse(priceAsString);

            return price;
        }

        private static string GetDescription(IDocument document)
        {
            var description = document.QuerySelectorAll("td").Where(x =>
            {
                var styleContent = x.GetAttribute("style") ?? "";

                if (styleContent.Contains("line-height:24px; font-size:14px; color: #444;"))
                {
                    return true;
                }

                return false;
            }).FirstOrDefault()?.TextContent;

            return description;
        }

        private static int GetYear(IDocument document)
        {
            var yearAsString = document.QuerySelector(".dilarData").TextContent;
            yearAsString = Regex.Match(yearAsString, @"(\d+)г\.").Groups[1].Value;

            var year = int.Parse(yearAsString);

            return year;
        }

        private static string GetEngineType(IDocument document)
        {
            var engineType = document.QuerySelectorAll(".dilarData li").Where(x => x.TextContent == "Тип двигател");

            return "";
        }
    }
}
