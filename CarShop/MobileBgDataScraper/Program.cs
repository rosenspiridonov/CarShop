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

    class Program
    {
        private static IConfiguration config;
        private static IBrowsingContext context;

        static void Main(string[] args)
        {
            config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);

            var car = GetCar("https://www.mobile.bg/pcgi/mobile.cgi?act=4&adv=11626433709764309");// Alfa
            var car2 = GetCar("https://www.mobile.bg/pcgi/mobile.cgi?act=4&adv=11620798005281686"); // Golf 
            var car3 = GetCar("https://www.mobile.bg/pcgi/mobile.cgi?act=4&adv=21626358782196243"); // Acura
            var car4 = GetCar("https://www.mobile.bg/pcgi/mobile.cgi?act=4&adv=11625407559751380"); // A3
            var car5 = GetCar("https://www.mobile.bg/pcgi/mobile.cgi?act=4&adv=11626702518884978"); // Tesla

            return;

            // Iterate through pages with posts
            Parallel.For(1, 10, (i) =>
            {
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
            car.HorsePower = GetHorsePower(document);
            car.EuroStandard = GetEuroStandard(document);
            car.Transmision = GetTransmisionType(document);
            car.CoupeType = GetCoupeType(document);
            car.TravelledDistance = GetTravelledDistance(document);
            car.Color = GetColor(document);

            car.SafetyProperties = GetSafetyProps(document);

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
            var modelRegex = new Regex(@"'AdvertModel':\s?\['(.*?)'\]");

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
            var html = document.DocumentElement.OuterHtml;
            var engineType = Regex
                .Match(html, @"'AdvertEngineType':\s?\['(.*?)'\]")
                .Groups[1]
                .Value;

            return engineType;
        }

        private static int? GetHorsePower(IDocument document)
        {
            var hpAsString = document.QuerySelector(".dilarData").TextContent;
            hpAsString = Regex.Match(hpAsString, @"(\d+)\s? к\.с\.").Groups[1].Value;

            var hp = 0;
            int.TryParse(hpAsString, out hp);

            if (hp == 0)
            {
                return null;
            }

            return hp;
        }

        private static string GetEuroStandard(IDocument document)
        {
            var dilarData = document.QuerySelector(".dilarData").InnerHtml;
            var euroStandard = Regex
                .Match(dilarData, @"<li>Евростандарт<\/li><li>(.* \d)<\/li>")
                .Groups[1]
                .Value;

            return euroStandard == "" ? null : euroStandard;
        }

        private static string GetTransmisionType(IDocument document)
        {
            var dilarData = document.QuerySelector(".dilarData").InnerHtml;
            var transmisionType = Regex
                .Match(dilarData, @"<li>Скоростна кутия<\/li><li>(.+?)<\/li>")
                .Groups[1]
                .Value;

            return transmisionType == "" ? null : transmisionType;
        }

        private static string GetCoupeType(IDocument document)
        {
            var dilarData = document.QuerySelector(".dilarData").InnerHtml;
            var coupeType = Regex
                .Match(dilarData, @"<li>Категория<\/li><li>(.+?)<\/li>")
                .Groups[1]
                .Value;

            return coupeType == "" ? null : coupeType;
        }

        private static int? GetTravelledDistance(IDocument document)
        {
            var dilarData = document.QuerySelector(".dilarData").InnerHtml;
            var travelledDistanceAsString = Regex
                .Match(dilarData, @"<li>Пробег<\/li><li>(.+?)<\/li>")
                .Groups[1]
                .Value
                .Replace("км", "")
                .Trim('.', ' ');

            var travelledDistance = 0;

            int.TryParse(travelledDistanceAsString, out travelledDistance);

            if (travelledDistance == 0)
            {
                return null;
            }

            return travelledDistance;
        }

        private static string GetColor(IDocument document)
        {
            var dilarData = document.QuerySelector(".dilarData").InnerHtml;
            var color = Regex
                .Match(dilarData, @"<li>Цвят<\/li><li>(.+?)<\/li>")
                .Groups[1]
                .Value;

            return color == "" ? null : color;
        }

        private static IEnumerable<string> GetSafetyProps(IDocument document)
        {
            var table = document.QuerySelector(".newAdImages + div + div + div + table").InnerHtml;

            return new List<string>();
        }
    }
}
