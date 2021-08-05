namespace CarShop.Services.Cars
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarShop.Web.Data.Models;
    using CarShop.Web.Data;
    using CarShop.Web.Models.Cars;
    using CarShop.Web.Services.Cars;
    using Microsoft.Data.Sql;

    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;

        public CarsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<int> CreateCarAsync(string ownerId, CarInputModel input)
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
                SafetyProperties = input.SafetyPropertiesIds?.Select(p => db.SafetyProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                ComfortProperties = input.ComfortPropertiesIds?.Select(p => db.ComfortProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                OtherProperties = input.OtherPropertiesIds?.Select(p => db.OtherProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                ExteriorProperties = input.ExteriorPropertiesIds?.Select(p => db.ExteriorProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                InteriorProperties = input.InteriorPropertiesIds?.Select(p => db.InteriorProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                ProtectionProperties = input.ProtectionPropertiesIds?.Select(p => db.ProtectionProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
                SpecialProperties = input.SpecialPropertiesIds?.Select(p => db.SpecialProperties.FirstOrDefault(pp => pp.Id == p)).ToList(),
            };

            car.OwnerId = ownerId;

            await db.Cars.AddAsync(car);
            await db.SaveChangesAsync();

            return car.Id;
        }

        public IEnumerable<CarServiceModel> GetCars(int start, int count)
        {
            return db.Cars
                .Select(x => new CarServiceModel
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
                .Skip(start)
                .Take(count)
                .OrderBy(x => x.Brand)
                .ThenBy(x => x.Model)
                .ThenBy(x => x.Modification)
                .ToList();
        }

        public CarFormData AllCarOptions()
            => new CarFormData
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

        public CarServiceModel GetCarViewModel(int id)
            => db.Cars
                .Where(x => x.Id == id)
                .Select(x => new CarServiceModel
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

        public CarInputModel CarInputModelInfo(int id)
            => db
                .Cars
                .Where(x => x.Id == id)
                .Select(x => new CarInputModel()
                {
                    Id = x.Id,
                    BrandId = x.BrandId,
                    ModelId = x.ModelId,
                    Modification = x.Modification,
                    Price = x.Price,
                    Description = x.Description,
                    ProduceYear = x.ProduceYear,
                    EngineTypeId = x.EngineTypeId,
                    HorsePower = x.HorsePower,
                    EuroStandardId = x.EuroStandardId,
                    TransmisionTypeId = x.TransmisionId,
                    CoupeTypeId = x.CoupeTypeId,
                    TravelledDistance = x.TravelledDistance,
                    Color = x.Color,
                    ImageUrl = x.Image.Url,
                    SafetyPropertiesIds = x.SafetyProperties.Select(x => x.Id).ToList(),
                    ComfortPropertiesIds = x.ComfortProperties.Select(x => x.Id).ToList(),
                    OtherPropertiesIds = x.OtherProperties.Select(x => x.Id).ToList(),
                    ExteriorPropertiesIds = x.ExteriorProperties.Select(x => x.Id).ToList(),
                    InteriorPropertiesIds = x.InteriorProperties.Select(x => x.Id).ToList(),
                    ProtectionPropertiesIds = x.ProtectionProperties.Select(x => x.Id).ToList(),
                    SpecialPropertiesIds = x.SpecialProperties.Select(x => (int?)x.Id).ToList(),
                    OwnerId = x.OwnerId
                }).FirstOrDefault();

        public async Task<int> EditCarAsync(CarInputModel input)
        {
            var car = db.Cars.Find(input.Id);
            db.Cars.Remove(car);

            int carId = await CreateCarAsync(input.OwnerId, input);
            return carId;
            //car.BrandId = input.BrandId;
            //car.ModelId = input.ModelId;
            //car.Modification = input.Modification;
            //car.Price = input.Price;
            //car.Description = input.Description;
            //car.ProduceYear = input.ProduceYear;
            //car.EngineTypeId = input.EngineTypeId;
            //car.HorsePower = input.HorsePower;
            //car.EuroStandardId = input.EuroStandardId;
            //car.TransmisionId = input.TransmisionTypeId;
            //car.CoupeTypeId = input.CoupeTypeId;
            //car.TravelledDistance = input.TravelledDistance;
            //car.Color = input.Color;
            //car.Image = new Image { Url = input.ImageUrl };
            //car.SafetyProperties = input.SafetyPropertiesIds?.Select(p => db.SafetyProperties.FirstOrDefault(pp => pp.Id == p)).ToList();
            //car.ComfortProperties = input.ComfortPropertiesIds?.Select(p => db.ComfortProperties.FirstOrDefault(pp => pp.Id == p)).ToList();
            //car.OtherProperties = input.OtherPropertiesIds?.Select(p => db.OtherProperties.FirstOrDefault(pp => pp.Id == p)).ToList();
            //car.ExteriorProperties = input.ExteriorPropertiesIds?.Select(p => db.ExteriorProperties.FirstOrDefault(pp => pp.Id == p)).ToList();
            //car.InteriorProperties = input.InteriorPropertiesIds?.Select(p => db.InteriorProperties.FirstOrDefault(pp => pp.Id == p)).ToList();
            //car.ProtectionProperties = input.ProtectionPropertiesIds?.Select(p => db.ProtectionProperties.FirstOrDefault(pp => pp.Id == p)).ToList();
            //car.SpecialProperties = input.SpecialPropertiesIds?.Select(p => db.SpecialProperties.FirstOrDefault(pp => pp.Id == p)).ToList();

            //await db.SaveChangesAsync();
        }
    }
}
