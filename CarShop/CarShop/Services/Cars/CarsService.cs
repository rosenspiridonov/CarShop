namespace CarShop.Services.Cars
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarShop.Data;
    using CarShop.Models.Cars;
    using CarShop.Data.Models;

    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;

        public CarsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<Car> CreateCarAsync(CarInputModel input)
        {
            var car = new Car
            {
                BrandId = input.BrandId,
                ModelId = input.ModelId,
                Modification = input.Modification,
                Price = input.Price,
                Description = input.Description,
                ProduceYear = input.ProduceYear,
                EngineTypeId = input.EngineTypeId,
                HorsePower = input.HorsePower,
                EuroStandardId = input.EuroStandardId,
                TransmisionId = input.TransmisionTypeId,
                CoupeTypeId = input.CoupeTypeId,
                TravelledDistance = input.TravelledDistance,
                Color = input.Color,
                Image = new Image { Url = input.ImageUrl },
                SafetyProperties = input.SafetyPropertiesIds.Select(p => db.SafetyProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                ComfortProperties = input.ComfortPropertiesIds.Select(p => db.ComfortProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                OtherProperties = input.OtherPropertiesIds.Select(p => db.OtherProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                ExteriorProperties = input.ExteriorPropertiesIds.Select(p => db.ExteriorProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                InteriorProperties = input.InteriorPropertiesIds.Select(p => db.InteriorProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                ProtectionProperties = input.ProtectionPropertiesIds.Select(p => db.ProtectionProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                SpecialProperties = input.SpecialPropertiesIds is null ? null : input.SpecialPropertiesIds.Select(p => db.SpecialProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
            };

            await db.Cars.AddAsync(car);
            await db.SaveChangesAsync();

            return car;
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
