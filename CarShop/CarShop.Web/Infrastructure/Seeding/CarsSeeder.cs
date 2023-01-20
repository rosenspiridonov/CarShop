namespace CarShop.Web.Infrastructure.Seeding
{
    using CarShop.Web.Data;
    using MobileBgDataScraper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarShop.Web.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class CarsSeeder
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<IdentityUser> userManager;

        public CarsSeeder(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task ProcessCars()
        {
            var carsService = new DataScraper();

            var cars = carsService.PopulateCars(1, 50);

            var carEntites = await CreateCarsAsync(cars);
            await AddCarsAsync(carEntites);
        }

        private async Task<IEnumerable<Car>> CreateCarsAsync(IEnumerable<CarDto> cars)
        {
            var entities = new List<Car>();
            var owner = await userManager.FindByEmailAsync("admin@carshop.com");

            foreach (var car in cars)
            {

                try
                {
                    var newCar = await CreateCarAsync(car, owner);
                    entities.Add(newCar);
                }
                catch (ArgumentNullException)
                { }

            }

            return entities;
        }

        private async Task<Car> CreateCarAsync(CarDto car, IdentityUser owner)
        {
            var newCar = new Car();

            newCar.Modification = car.Modification;
            newCar.Price = car.Price;
            newCar.Description = car.Description;
            newCar.ProduceYear = car.ProduceYear;
            newCar.HorsePower = car.HorsePower;
            newCar.Color = car.Color;
            newCar.TravelledDistance = car.TravelledDistance;
            newCar.BrandId = await GetBrandIdAsync(car.Brand);
            newCar.ModelId = await GetModelIdAsync(car.Model, newCar.BrandId);
            newCar.EngineTypeId = await GetEngineTypeIdAsync(car.EngineType);
            newCar.EuroStandardId = await GetEuroStandardIdAsync(car.EuroStandard);
            newCar.TransmisionId = await GetTransmisionIdAsync(car.Transmision);
            newCar.CoupeTypeId = await GetCoupeTypeIdAsync(car.CoupeType);
            newCar.ImageId = await GetImageIdAsync(car.ImageUrl);
            newCar.SafetyProperties = await GetSefetyPropsAsync(car.SafetyProperties);
            newCar.ComfortProperties = await GetComfortPropsAsync(car.ComfortProperties);
            newCar.OtherProperties = await GetOtherPropsAsync(car.OtherProperties);
            newCar.ExteriorProperties = await GetExteriorPropsAsync(car.ExteriorProperties);
            newCar.InteriorProperties = await GetInteriorPropsAsync(car.InteriorProperties);
            newCar.ProtectionProperties = await GetProtectionPropsAsync(car.ProtectionProperties);
            newCar.SpecialProperties = await GetSpecialPropsAsync(car.SpecialProperties);
            newCar.Owner = owner;

            return newCar;
        }

        private async Task AddCarsAsync(IEnumerable<Car> cars)
        {
            db.Cars.AddRange(cars);
            await db.SaveChangesAsync();
        }

        private async Task<ICollection<Special>> GetSpecialPropsAsync(ICollection<string> specialProperties)
        {
            var result = new List<Special>();

            foreach (var prop in specialProperties)
            {
                var newProp = db
                .SpecialProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp == null)
                {
                    var newRecord = new Special() { Name = prop };
                    db.SpecialProperties.Add(newRecord);
                    await db.SaveChangesAsync();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private async Task<ICollection<Protection>> GetProtectionPropsAsync(ICollection<string> protectionProperties)
        {
            var result = new List<Protection>();

            foreach (var prop in protectionProperties)
            {
                var newProp = db
                .ProtectionProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp == null)
                {
                    var newRecord = new Protection() { Name = prop };
                    db.ProtectionProperties.Add(newRecord);
                    await db.SaveChangesAsync();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private async Task<ICollection<Interior>> GetInteriorPropsAsync(ICollection<string> interiorProperties)
        {
            var result = new List<Interior>();

            foreach (var prop in interiorProperties)
            {
                var newProp = db
                .InteriorProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp == null)
                {
                    var newRecord = new Interior() { Name = prop };
                    db.InteriorProperties.Add(newRecord);
                    await db.SaveChangesAsync();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private async Task<ICollection<Exterior>> GetExteriorPropsAsync(ICollection<string> exteriorProperties)
        {
            var result = new List<Exterior>();

            foreach (var prop in exteriorProperties)
            {
                var newProp = db
                .ExteriorProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp == null)
                {
                    var newRecord = new Exterior() { Name = prop };
                    db.ExteriorProperties.Add(newRecord);
                    await db.SaveChangesAsync();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private async Task<ICollection<Other>> GetOtherPropsAsync(ICollection<string> otherProperties)
        {
            var result = new List<Other>();

            foreach (var prop in otherProperties)
            {
                var newProp = db
                .OtherProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp == null)
                {
                    var newRecord = new Other() { Name = prop };
                    db.OtherProperties.Add(newRecord);
                    await db.SaveChangesAsync();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private async Task<ICollection<Comfort>> GetComfortPropsAsync(ICollection<string> comfortProperties)
        {
            var result = new List<Comfort>();

            foreach (var prop in comfortProperties)
            {
                var newProp = db
                .ComfortProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp == null)
                {
                    var newRecord = new Comfort() { Name = prop };
                    db.ComfortProperties.Add(newRecord);
                    await db.SaveChangesAsync();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private async Task<ICollection<Safety>> GetSefetyPropsAsync(ICollection<string> safetyProperties)
        {
            var result = new List<Safety>();

            foreach (var prop in safetyProperties)
            {
                var newProp = db
                .SafetyProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp == null)
                {
                    var newRecord = new Safety() { Name = prop };
                    db.SafetyProperties.Add(newRecord);
                    await db.SaveChangesAsync();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private async Task<int> GetImageIdAsync(string imageUrl)
        {
            if (imageUrl == null)
            {
                throw new ArgumentNullException(nameof(imageUrl));
            }

            var id = db
                .Images
                .FirstOrDefault(x => x.Url == imageUrl)
                ?.Id;

            if (id == null)
            {
                var newRecord = new Image() { Url = imageUrl };
                db.Images.Add(newRecord);
                await db.SaveChangesAsync();
                id = newRecord.Id;
            }

            return id.Value;
        }

        private async Task<int> GetCoupeTypeIdAsync(string coupeType)
        {
            if (coupeType == null)
            {
                throw new ArgumentNullException(nameof(coupeType));
            }

            var id = db
                .CoupeTypes
                .FirstOrDefault(x => x.Name == coupeType)
                ?.Id;

            if (id == null)
            {
                var newRecord = new Coupe() { Name = coupeType };
                db.CoupeTypes.Add(newRecord);
                await db.SaveChangesAsync();
                id = newRecord.Id;
            }

            return id.Value;
        }

        private async Task<int> GetTransmisionIdAsync(string transmision)
        {
            if (transmision == null)
            {
                throw new ArgumentNullException(nameof(transmision));
            }

            var id = db
                .TransmisionTypes
                .FirstOrDefault(x => x.Name == transmision)
                ?.Id;

            if (id == null)
            {
                var newRecord = new Transmision() { Name = transmision };
                db.TransmisionTypes.Add(newRecord);
                await db.SaveChangesAsync();
                id = newRecord.Id;
            }

            return id.Value;
        }

        private async Task<int?> GetEuroStandardIdAsync(string euroStandard)
        {
            if (euroStandard == null)
            {
                return null;
            }

            var id = db
                .EuroStandards
                .FirstOrDefault(x => x.Name == euroStandard)
                ?.Id;

            if (id == null)
            {
                var newRecord = new EuroStandard() { Name = euroStandard };
                db.EuroStandards.Add(newRecord);
                await db.SaveChangesAsync();
                id = newRecord.Id;
            }

            return id.Value;
        }

        private async Task<int> GetEngineTypeIdAsync(string engineType)
        {
            if (engineType == null)
            {
                throw new ArgumentNullException(nameof(engineType));
            }

            var id = db
                .EngineTypes
                .FirstOrDefault(x => x.Name == engineType)
                ?.Id;

            if (id == null)
            {
                var newRecord = new Engine() { Name = engineType };
                db.EngineTypes.Add(newRecord);
                await db.SaveChangesAsync();
                id = newRecord.Id;
            }

            return id.Value;
        }

        private async Task<int> GetModelIdAsync(string model, int brandId)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var id = db
                .Models
                .FirstOrDefault(x => x.Name == model && x.BrandId == brandId)
                ?.Id;

            if (id == null)
            {
                var newRecord = new Model() { Name = model, BrandId = brandId };
                db.Models.Add(newRecord);
                await db.SaveChangesAsync();
                id = newRecord.Id;
            }

            return id.Value;
        }

        private async Task<int> GetBrandIdAsync(string brand)
        {
            if (brand == null)
            {
                throw new ArgumentNullException(nameof(brand));
            }

            var id = db
                .Brands
                .FirstOrDefault(x => x.Name == brand)
                ?.Id;

            if (id == null)
            {
                var newRecord = new Brand() { Name = brand };
                db.Brands.Add(newRecord);
                await db.SaveChangesAsync();
                id = newRecord.Id;
            }
            
            return id.Value;
        }
    }
}
