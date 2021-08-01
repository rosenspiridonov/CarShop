namespace CarShop.Services.Cars
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarShop.Web.Data.Models;
    using CarShop.Web.Data;
    using CarShop.Web.Models.Cars;

    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;

        public CarsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<Car> CreateCarAsync(string ownerId, CarInputModel input)
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
                SafetyProperties = input.SafetyPropertiesIds is null ? null : input.SafetyPropertiesIds.Select(p => db.SafetyProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                ComfortProperties = input.ComfortPropertiesIds is null ? null : input.ComfortPropertiesIds.Select(p => db.ComfortProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                OtherProperties = input.OtherPropertiesIds is null ? null : input.OtherPropertiesIds.Select(p => db.OtherProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                ExteriorProperties = input.ExteriorPropertiesIds is null ? null : input.ExteriorPropertiesIds.Select(p => db.ExteriorProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                InteriorProperties = input.InteriorPropertiesIds is null ? null : input.InteriorPropertiesIds.Select(p => db.InteriorProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                ProtectionProperties = input.ProtectionPropertiesIds is null ? null : input.ProtectionPropertiesIds.Select(p => db.ProtectionProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                SpecialProperties = input.SpecialPropertiesIds is null ? null : input.SpecialPropertiesIds.Select(p => db.SpecialProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
            };

            car.OwnerId = ownerId;

            await db.Cars.AddAsync(car);
            await db.SaveChangesAsync();

            return car;
        }

        public IEnumerable<CarViewModel> GetCars(int start, int count)
        {
            return db.Cars
                .Skip(start)
                .Take(count)
                .Select(x => new CarViewModel
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
                    SafetyProperties = x.SafetyProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                    ComfortProperties = x.ComfortProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                    OtherProperties = x.OtherProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                    ExteriorProperties = x.ExteriorProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                    InteriorProperties = x.InteriorProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                    ProtectionProperties = x.ProtectionProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                    SpecialProperties = x.SpecialProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                })
                .OrderBy(x => x.Brand)
                .ThenBy(x => x.Model)
                .ThenBy(x => x.Modification)
                .ToList();
        }

        public CreateCarViewData GetCarEntities()
            => new CreateCarViewData
            {
                Brands = db.Brands.OrderBy(x => x.Name).ToList(),
                Models = db.Models.OrderBy(x => x.Name).ToList(),
                EngineTypes = db.EngineTypes.OrderBy(x => x.Name).ToList(),
                EuroStandards = db.EuroStandards.OrderBy(x => x.Name).ToList(),
                TransmisionTypes = db.TransmisionTypes.OrderBy(x => x.Name).ToList(),
                CoupeTypes = db.CoupeTypes.OrderBy(x => x.Name).ToList(),
                SafetyProperties = db.SafetyProperties.OrderBy(x => x.Name).ToList(),
                ComfortProperties = db.ComfortProperties.OrderBy(x => x.Name).ToList(),
                OtherProperties = db.OtherProperties.OrderBy(x => x.Name).ToList(),
                ExteriorProperties = db.ExteriorProperties.OrderBy(x => x.Name).ToList(),
                InteriorProperties = db.InteriorProperties.OrderBy(x => x.Name).ToList(),
                ProtectionProperties = db.ProtectionProperties.OrderBy(x => x.Name).ToList(),
                SpecialProperties = db.SpecialProperties.OrderBy(x => x.Name).ToList(),
            };

        public CarViewModel GetCar(int id)
            => db.Cars
                .Where(x => x.Id == id)
                .Select(x => new CarViewModel
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
                    OwnerId = x.OwnerId,
                    IsActive = x.IsActive,
                    SafetyProperties = x.SafetyProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                    ComfortProperties = x.ComfortProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                    OtherProperties = x.OtherProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                    ExteriorProperties = x.ExteriorProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                    InteriorProperties = x.InteriorProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                    ProtectionProperties = x.ProtectionProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                    SpecialProperties = x.SpecialProperties.Select(x => x.Name).OrderBy(x => x).ToList(),
                }).FirstOrDefault();
    }
}
