namespace CarShop.Services.Cars
{
    using CarShop.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;

        public CarsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CarServiceModel> GetCars(int start, int count)
        {
            return db.Cars.Skip(start).Select(x => new CarServiceModel
            {
                Id = x.Id,
                Brand = x.Brand.Name,
                Model = x.Model.Name,
                Modification = x.Modification,
                Price = x.Price,
                Description = x.Description,
                ProduceYear = x.ProduceYear,
                EngineType = x.EngineType.Name,
                HorsePower = x.HorsePower,
                EuroStandard = x.EuroStandard.Name,
                TransmisionType = x.Transmision.Name,
                CoupeType = x.CoupeType.Name,
                TravelledDistance = x.TravelledDistance,
                Color = x.Color,
                ImageUrl = x.Image.Url,
                SafetyProperties = x.SafetyProperties.Select(x => x.Name),
                ComfortProperties = x.ComfortProperties.Select(x => x.Name),
                OtherProperties = x.OtherProperties.Select(x => x.Name),
                ExteriorProperties = x.ExteriorProperties.Select(x => x.Name),
                InteriorProperties = x.InteriorProperties.Select(x => x.Name),
                ProtectionProperties = x.ProtectionProperties.Select(x => x.Name),
                SpecialProperties = x.SpecialProperties.Select(x => x.Name),
            })
            .Take(count)
            .OrderBy(x => x.Brand)
            .ThenBy(x => x.Model)
            .ThenBy(x => x.Modification)
            .ToList();
        }
    }
}
