namespace MobileBgDataScraper
{
    using AngleSharp;
    using AngleSharp.Dom;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class DataScraper
    {
        private const string MOBILE_BG_SEARCH_URL = "https://www.mobile.bg/pcgi/mobile.cgi?act=3&slink=r1yqba&f1={0}";

        private readonly IConfiguration config;
        private readonly IBrowsingContext context;

        public DataScraper()
        {
            this.config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(config);
        }

        public IEnumerable<CarDto> PopulateCars(int startPage, int endPage)
        {
            var cars = new List<CarDto>();

            // Iterate through pages with posts
            Parallel.For(startPage, endPage, (i) =>
            {
                var url = string.Format(MOBILE_BG_SEARCH_URL, i);
                var postUrls = GetPostUrlsAsync(url).GetAwaiter().GetResult();

                // Iterate through all post on the page
                Parallel.ForEach(postUrls, (post) =>
                {
                    try
                    {

                        var car = GetCarAsync(post).GetAwaiter().GetResult();

                        if (car is not null)
                        {
                            cars.Add(car);
                        }

                        //Console.WriteLine("Parsed...");
                    }
                    catch (Exception)
                    {
                        //Console.WriteLine("Error...");
                    }

                });
            });

            return cars;
        }

        private async Task<CarDto> GetCarAsync(string url)
        {
            var document = await context.OpenAsync(url);

            if (document.DocumentElement.TextContent == string.Empty)
            {
                return null;
            }

            var car = new CarDto();

            try
            {
                car.Model = GetModel(document);
                car.Brand = GetBrand(document);
                car.Modification = GetModification(document);

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
                car.ImageUrl = GetImageUrl(document);

                car = GetProps(car, document);
            }
            catch (Exception)
            {

            }

            return car;
        }

        private async Task<IEnumerable<string>> GetPostUrlsAsync(string url)
        {
            var document = await context.OpenAsync(url);

            var postsUrls = document.QuerySelectorAll(".photoLink")
                .Select(x => x.GetAttribute("href"))
                .Select(x => x = "https:" + x)
                .ToArray();

            return postsUrls;
        }

        private string GetBrand(IDocument document)
        {
            var html = document.DocumentElement.OuterHtml;
            var brandRegex = new Regex(@"'AdvertBrand':\s?\['([A-z\s]+)'\]");
            var brandMatch = brandRegex.Match(html);

            return brandMatch.Groups[1].ToString();
        }

        private string GetModel(IDocument document)
        {
            var html = document.DocumentElement.OuterHtml;
            var modelRegex = new Regex(@"'AdvertModel':\s?\['(.*?)'\]");
            var modelMatch = modelRegex.Match(html);

            return modelMatch.Groups[1].ToString();
        }

        private string GetModification(IDocument document)
        {
            var titleLine = document.QuerySelector("h1")?.TextContent;
            var brand = GetBrand(document);
            var model = GetModel(document);
            var modification = titleLine?
            .Replace(brand, string.Empty)
            .Replace(model, string.Empty)
            .Trim();

            return string.IsNullOrEmpty(modification) ? null : modification;
        }

        private decimal GetPrice(IDocument document)
        {
            var priceAsString = document.QuerySelector("#details_price")
                .TextContent;

            priceAsString = Regex.Match(priceAsString, @"[0-9\s]+")
                .Value
                .Replace(" ", string.Empty);

            var price = decimal.Parse(priceAsString);

            return price;
        }

        private string GetDescription(IDocument document)
        {
            var description = document.QuerySelectorAll("td").Where(x =>
            {
                var styleContent = x.GetAttribute("style") ?? string.Empty;
                return styleContent.Contains("line-height:24px; font-size:14px; color: #444;");
            }).FirstOrDefault()?.TextContent;

            return description;
        }

        private int GetYear(IDocument document)
        {
            var yearAsString = document.QuerySelector(".dilarData").TextContent;
            yearAsString = Regex.Match(yearAsString, @"(\d+)г\.").Groups[1].Value;

            var year = int.Parse(yearAsString);

            return year;
        }

        private string GetEngineType(IDocument document)
        {
            var html = document.DocumentElement.OuterHtml;
            var engineType = Regex
                .Match(html, @"'AdvertEngineType':\s?\['(.*?)'\]")
                .Groups[1]
                .Value;

            return engineType;
        }

        private int? GetHorsePower(IDocument document)
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

        private string GetEuroStandard(IDocument document)
        {
            var dilarData = document.QuerySelector(".dilarData").InnerHtml;
            var euroStandard = Regex
                .Match(dilarData, @"<li>Евростандарт<\/li><li>(.* \d)<\/li>")
                .Groups[1]
                .Value;

            return string.IsNullOrEmpty(euroStandard) ? null : euroStandard;
        }

        private string GetTransmisionType(IDocument document)
        {
            var dilarData = document.QuerySelector(".dilarData").InnerHtml;
            var transmisionType = Regex
                .Match(dilarData, @"<li>Скоростна кутия<\/li><li>(.+?)<\/li>")
                .Groups[1]
                .Value;

            return string.IsNullOrEmpty(transmisionType) ? null : transmisionType;
        }

        private string GetCoupeType(IDocument document)
        {
            var dilarData = document.QuerySelector(".dilarData").InnerHtml;
            var coupeType = Regex
                .Match(dilarData, @"<li>Категория<\/li><li>(.+?)<\/li>")
                .Groups[1]
                .Value;

            return string.IsNullOrEmpty(coupeType) ? null : coupeType;
        }

        private int? GetTravelledDistance(IDocument document)
        {
            var dilarData = document.QuerySelector(".dilarData").InnerHtml;
            var travelledDistanceAsString = Regex
                .Match(dilarData, @"<li>Пробег[^<]*<\/li><li>(.+?)<\/li>")
                .Groups[1]
                .Value
                .Replace("км", string.Empty)
                .Trim('.', ' ');

            var travelledDistance = 0;

            int.TryParse(travelledDistanceAsString, out travelledDistance);

            if (travelledDistance == 0)
            {
                return null;
            }

            return travelledDistance;
        }

        private string GetColor(IDocument document)
        {
            var dilarData = document.QuerySelector(".dilarData").InnerHtml;
            var color = Regex
                .Match(dilarData, @"<li>Цвят<\/li><li>(.+?)<\/li>")
                .Groups[1]
                .Value;

            return string.IsNullOrEmpty(color) ? null : color;
        }

        private CarDto GetProps(CarDto car, IDocument document)
        {
            var propLines = document
                .QuerySelector(".newAdImages + div + div + div + table")?
                .TextContent
                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            if (propLines is null)
            {
                return car;
            }

            var currPropName = string.Empty;

            foreach (var line in propLines)
            {
                if (line.StartsWith("•"))
                {
                    var prop = line
                        .Replace("•", string.Empty)
                        .Trim();

                    car = AddPropToCorrectArray(prop, currPropName, car);
                }
                else
                {
                    var splitted = line
                        .Split("•", 2)
                        .Select(x => x = x.Trim())
                        .ToArray();

                    currPropName = splitted[0];

                    car = AddPropToCorrectArray(splitted[1], currPropName, car);
                }
            }

            return car;
        }

        private CarDto AddPropToCorrectArray(string prop, string propName, CarDto car)
        {
            switch (propName)
            {
                case "Безопасност":
                    car.SafetyProperties.Add(prop);
                    break;
                case "Комфорт":
                    car.ComfortProperties.Add(prop);
                    break;
                case "Други":
                    car.OtherProperties.Add(prop);
                    break;
                case "Екстериор":
                    car.ExteriorProperties.Add(prop);
                    break;
                case "Защита":
                    car.ProtectionProperties.Add(prop);
                    break;
                case "Интериор":
                    car.InteriorProperties.Add(prop);
                    break;
                case "Специализирани":
                    car.SpecialProperties.Add(prop);
                    break;
                default:
                    break;
            }

            return car;
        }

        private string GetImageUrl(IDocument document)
        {
            var imageUrl = document.QuerySelector(".imgHolder > img")?.GetAttribute("src");
            imageUrl = "https:" + imageUrl;
            return imageUrl;
        }
    }
}
