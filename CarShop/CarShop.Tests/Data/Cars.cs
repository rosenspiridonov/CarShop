namespace CarShop.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CarShop.Web.Data.Models;
    using CarShop.Web.Models.Cars;

    using MyTested.AspNetCore.Mvc;

    public static class Cars
    {
        public static IEnumerable<Car> FiveCars
            => Enumerable.Range(0, 5).Select(x => new Car());

        public static CarFormModel ValidCarFormModel
            => new()
            {
                Car = new CarInputModel()
                {
                    Id = TestCar.Id,
                    BrandId = 1,
                    ModelId = 3,
                    Modification = "Test",
                    Price = 20000,
                    Description = "Test",
                    ProduceYear = 2021,
                    EngineTypeId = 1,
                    HorsePower = 480,
                    EuroStandardId = 1,
                    TransmisionTypeId = 1,
                    CoupeTypeId = 1,
                    TravelledDistance = 20000,
                    Color = "Test",
                    ImageUrl = @"https://test.com/img/test.jpg",
                    SafetyPropertiesIds = null,
                    ComfortPropertiesIds = null,
                    OtherPropertiesIds = null,
                    ExteriorPropertiesIds = null,
                    InteriorPropertiesIds = null,
                    ProtectionPropertiesIds = null,
                    SpecialPropertiesIds = null,
                }
            };

        public static Car TestCar
            => new()
            {
                Id = 1,
                OwnerId = TestUser.Identifier,
                Brand = new() { Name = "Test" },
                Model = new() { Name = "Test" },
                Modification = "Test",
                Price = 20000,
                Description = "Test",
                ProduceYear = 2021,
                EngineType = new() { Name = "Test" },
                HorsePower = 500,
                EuroStandard = new() { Name = "Test" },
                Transmision = new() { Name = "Test" },
                CoupeType = new() { Name = "Test" },
                TravelledDistance = 20000,
                Color = "Test",
                Image = new() { Url = "https://test.com/img/test.jpg" },
            };
    }
}
