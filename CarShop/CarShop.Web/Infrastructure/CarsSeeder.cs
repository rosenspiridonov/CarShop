namespace CarShop.Infrastructure
{
    using CarShop.Web.Data;
    using MobileBgDataScraper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarShop.Web.Data.Models;

    public class CarsSeeder
    {
        private readonly ApplicationDbContext db;

        public CarsSeeder(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void ProcessCars()
        {
            var carsService = new DataScraper();

            var cars = carsService.PopulateCars(1, 10);

            var carsSeeder = new CarsSeeder(db);

            foreach (var car in cars)
            {
                try
                {
                    carsSeeder.SeedCar(car);
                }
                catch (Exception)
                { }
            }
        }

        private void SeedCar(CarDto car)
        {
            var newCar = new Car();

            newCar.BrandId = GetBrandId(car.Brand);
            newCar.ModelId = GetModelId(car.Model, newCar.BrandId);
            newCar.Modification = car.Modification;
            newCar.Price = car.Price;
            newCar.Description = car.Description;
            newCar.ProduceYear = car.ProduceYear;
            newCar.EngineTypeId = GetEngineTypeId(car.EngineType);
            newCar.HorsePower = car.HorsePower;
            newCar.EuroStandardId = GetEuroStandardId(car.EuroStandard);
            newCar.TransmisionId = GetTransmisionId(car.Transmision);
            newCar.CoupeTypeId = GetCoupeTypeId(car.CoupeType);
            newCar.TravelledDistance = car.TravelledDistance;
            newCar.Color = car.Color;
            newCar.ImageId = GetImageId(car.ImageUrl);
            newCar.SafetyProperties = GetSefetyProps(car.SafetyProperties);
            newCar.ComfortProperties = GetComfortProps(car.ComfortProperties);
            newCar.OtherProperties = GetOtherProps(car.OtherProperties);
            newCar.ExteriorProperties = GetExteriorProps(car.ExteriorProperties);
            newCar.InteriorProperties = GetInteriorProps(car.InteriorProperties);
            newCar.ProtectionProperties = GetProtectionProps(car.ProtectionProperties);
            newCar.SpecialProperties = GetSpecialProps(car.SpecialProperties);

            db.Cars.Add(newCar);

            var newPost = new Post
            {
                Car = newCar
            };

            db.Posts.Add(newPost);

            db.SaveChanges();
        }

        private ICollection<Special> GetSpecialProps(ICollection<string> specialProperties)
        {
            var result = new List<Special>();

            foreach (var prop in specialProperties)
            {
                var newProp = db
                .SpecialProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp is null)
                {
                    var newRecord = new Special() { Name = prop };
                    db.SpecialProperties.Add(newRecord);
                    db.SaveChanges();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private ICollection<Protection> GetProtectionProps(ICollection<string> protectionProperties)
        {
            var result = new List<Protection>();

            foreach (var prop in protectionProperties)
            {
                var newProp = db
                .ProtectionProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp is null)
                {
                    var newRecord = new Protection() { Name = prop };
                    db.ProtectionProperties.Add(newRecord);
                    db.SaveChanges();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private ICollection<Interior> GetInteriorProps(ICollection<string> interiorProperties)
        {
            var result = new List<Interior>();

            foreach (var prop in interiorProperties)
            {
                var newProp = db
                .InteriorProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp is null)
                {
                    var newRecord = new Interior() { Name = prop };
                    db.InteriorProperties.Add(newRecord);
                    db.SaveChanges();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private ICollection<Exterior> GetExteriorProps(ICollection<string> exteriorProperties)
        {
            var result = new List<Exterior>();

            foreach (var prop in exteriorProperties)
            {
                var newProp = db
                .ExteriorProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp is null)
                {
                    var newRecord = new Exterior() { Name = prop };
                    db.ExteriorProperties.Add(newRecord);
                    db.SaveChanges();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private ICollection<Other> GetOtherProps(ICollection<string> otherProperties)
        {
            var result = new List<Other>();

            foreach (var prop in otherProperties)
            {
                var newProp = db
                .OtherProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp is null)
                {
                    var newRecord = new Other() { Name = prop };
                    db.OtherProperties.Add(newRecord);
                    db.SaveChanges();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private ICollection<Comfort> GetComfortProps(ICollection<string> comfortProperties)
        {
            var result = new List<Comfort>();

            foreach (var prop in comfortProperties)
            {
                var newProp = db
                .ComfortProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp is null)
                {
                    var newRecord = new Comfort() { Name = prop };
                    db.ComfortProperties.Add(newRecord);
                    db.SaveChanges();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private ICollection <Safety> GetSefetyProps(ICollection<string> safetyProperties)
        {
            var result = new List<Safety>();

            foreach (var prop in safetyProperties)
            {
                var newProp = db
                .SafetyProperties
                .FirstOrDefault(x => x.Name == prop);

                if (newProp is null)
                {
                    var newRecord = new Safety() { Name = prop };
                    db.SafetyProperties.Add(newRecord);
                    db.SaveChanges();
                    newProp = newRecord;
                }

                result.Add(newProp);
            }

            return result;
        }

        private int GetImageId(string imageUrl)
        {
            var id = db
                .Images
                .FirstOrDefault(x => x.Url == imageUrl)
                ?.Id;

            if (id is null)
            {
                var newRecord = new Image() { Url = imageUrl };
                db.Images.Add(newRecord);
                db.SaveChanges();
                id = newRecord.Id;
            }

            return (int)id;
        }

        private int GetCoupeTypeId(string coupeType)
        {
            var id = db
                .CoupeTypes
                .FirstOrDefault(x => x.Name == coupeType)
                ?.Id;

            if (id is null)
            {
                var newRecord = new Coupe() { Name = coupeType };
                db.CoupeTypes.Add(newRecord);
                db.SaveChanges();
                id = newRecord.Id;
            }

            return (int)id;
        }

        private int GetTransmisionId(string transmision)
        {
            var id = db
                .TransmisionTypes
                .FirstOrDefault(x => x.Name == transmision)
                ?.Id;

            if (id is null)
            {
                var newRecord = new Transmision() { Name = transmision };
                db.TransmisionTypes.Add(newRecord);
                db.SaveChanges();
                id = newRecord.Id;
            }

            return (int)id;
        }

        private int? GetEuroStandardId(string euroStandard)
        {
            if (euroStandard is null)
            {
                return null;
            }

            var id = db
                .EuroStandards
                .FirstOrDefault(x => x.Name == euroStandard)
                ?.Id;

            if (id is null)
            {
                var newRecord = new EuroStandard() { Name = euroStandard };
                db.EuroStandards.Add(newRecord);
                db.SaveChanges();
                id = newRecord.Id;
            }

            return (int)id;
        }

        private int GetEngineTypeId(string engineType)
        {
            var id = db
                .EngineTypes
                .FirstOrDefault(x => x.Name == engineType)
                ?.Id;

            if (id is null)
            {
                var newRecord = new Engine() { Name = engineType };
                db.EngineTypes.Add(newRecord);
                db.SaveChanges();
                id = newRecord.Id;
            }

            return (int)id;
        }

        private int GetModelId(string model, int brandId)
        {
            var id = db
                .Models
                .FirstOrDefault(x => x.Name == model && x.BrandId == brandId)
                ?.Id;

            if (id is null)
            {
                var newRecord = new Model() { Name = model, BrandId = brandId };
                db.Models.Add(newRecord);
                db.SaveChanges();
                id = newRecord.Id;
            }

            return (int)id;
        }

        private int GetBrandId(string brand)
        {
            var id = db
                .Brands
                .FirstOrDefault(x => x.Name == brand)
                ?.Id;

            if (id is null)
            {
                var newRecord = new Brand() { Name = brand };
                db.Brands.Add(newRecord);
                db.SaveChanges();
                id = newRecord.Id;
            }
            
            return (int)id;
        }
    }
}
