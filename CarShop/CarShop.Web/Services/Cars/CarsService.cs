using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using CarShop.Web.Data.Models;
using CarShop.Web.Data;
using CarShop.Web.Models.Cars;
using CarShop.Web.Services.Cars.Models;
using Microsoft.EntityFrameworkCore;
using CarShop.Web.Models.Sorting;
using CarShop.Web.Services.Images;

namespace CarShop.Services.Cars
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;
        private readonly IImagesService imageService;

        public CarsService(ApplicationDbContext db, IImagesService imageService)
        {
            this.db = db;
            this.imageService = imageService;
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
                IsDeleted = false,
                OwnerId = ownerId
            };

            if (!await imageService.IsValidAsync(car.Image.Url))
            {
                car.Image.Url = "/img/car-placeholder.jpg";
            }

            await db.Cars.AddAsync(car);
            await db.SaveChangesAsync();

            return car.Id;
        }

        public IEnumerable<CarServiceModel> GetCars(int start, int count)
        {
            return db.Cars
                .Where(x => x.IsDeleted == false)
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
            => new()
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
                    IsDeleted = x.IsDeleted
                }).FirstOrDefault();

        public CarInputModel CarInputModelInfo(int id)
            => db
                .Cars
                .Where(x => x.Id == id && x.IsDeleted == false)
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

        public void EditCar(CarInputModel input)
        {
            var car = db
                .Cars
                .Include(x => x.Image)
                .Include(x => x.SafetyProperties)
                .Include(x => x.ComfortProperties)
                .Include(x => x.OtherProperties)
                .Include(x => x.ExteriorProperties)
                .Include(x => x.InteriorProperties)
                .Include(x => x.ProtectionProperties)
                .Include(x => x.SpecialProperties)
                .FirstOrDefault(x => x.Id == input.Id);

            car.BrandId = input.BrandId;
            car.ModelId = input.ModelId;
            car.Modification = input.Modification;
            car.Price = input.Price;
            car.Description = input.Description;
            car.ProduceYear = input.ProduceYear;
            car.EngineTypeId = input.EngineTypeId;
            car.HorsePower = input.HorsePower;
            car.EuroStandardId = input.EuroStandardId;
            car.TransmisionId = input.TransmisionTypeId;
            car.CoupeTypeId = input.CoupeTypeId;
            car.TravelledDistance = input.TravelledDistance;
            car.Color = input.Color;
            car.Image.Url = input.ImageUrl;
            car.OwnerId = input.OwnerId;

            
            foreach (var propId in input.SafetyPropertiesIds ?? new())
            {
                if (!car.SafetyProperties.Any(x => x.Id == propId))
                {
                    car.SafetyProperties.Add(db.SafetyProperties.FirstOrDefault(x => x.Id == propId));
                }
            }
            
            car.SafetyProperties?
                .Where(p => !(input.SafetyPropertiesIds ?? new()).Any(id => id == p.Id))
                .ToList()
                .ForEach(p => car.SafetyProperties.Remove(p));

            foreach (var propId in input.ComfortPropertiesIds ?? new())
            {
                if (!car.ComfortProperties.Any(x => x.Id == propId))
                {
                    car.ComfortProperties.Add(db.ComfortProperties.FirstOrDefault(x => x.Id == propId));
                }
            }

            car.ComfortProperties?
                .Where(p => !(input.ComfortPropertiesIds ?? new()).Any(id => id == p.Id))
                .ToList()
                .ForEach(p => car.ComfortProperties.Remove(p));

            foreach (var propId in input.OtherPropertiesIds ?? new())
            {
                if (!car.OtherProperties.Any(x => x.Id == propId))
                {
                    car.OtherProperties.Add(db.OtherProperties.FirstOrDefault(x => x.Id == propId));
                }
            }

            car.OtherProperties?
                .Where(p => !(input.OtherPropertiesIds ?? new()).Any(id => id == p.Id))
                .ToList()
                .ForEach(p => car.OtherProperties.Remove(p));

            foreach (var propId in input.ExteriorPropertiesIds ?? new())
            {
                if (!car.ExteriorProperties.Any(x => x.Id == propId))
                {
                    car.ExteriorProperties.Add(db.ExteriorProperties.FirstOrDefault(x => x.Id == propId));
                }
            }

            car.ExteriorProperties?
                .Where(p => !(input.ExteriorPropertiesIds ?? new()).Any(id => id == p.Id))
                .ToList()
                .ForEach(p => car.ExteriorProperties.Remove(p));

            foreach (var propId in input.InteriorPropertiesIds ?? new())
            {
                if (!car.InteriorProperties.Any(x => x.Id == propId))
                {
                    car.InteriorProperties.Add(db.InteriorProperties.FirstOrDefault(x => x.Id == propId));
                }
            }

            car.InteriorProperties?
                .Where(p => !(input.InteriorPropertiesIds ?? new()).Any(id => id == p.Id))
                .ToList()
                .ForEach(p => car.InteriorProperties.Remove(p));

            foreach (var propId in input.ProtectionPropertiesIds ?? new())
            {
                if (!car.ProtectionProperties.Any(x => x.Id == propId))
                {
                    car.ProtectionProperties.Add(db.ProtectionProperties.FirstOrDefault(x => x.Id == propId));
                }
            }

            car.ProtectionProperties?
                .Where(p => !(input.ProtectionPropertiesIds ?? new()).Any(id => id == p.Id))
                .ToList()
                .ForEach(p => car.ProtectionProperties.Remove(p));

            foreach (var propId in input.SpecialPropertiesIds ?? new())
            {
                if (!car.SpecialProperties.Any(x => x.Id == propId))
                {
                    car.SpecialProperties.Add(db.SpecialProperties.FirstOrDefault(x => x.Id == propId));
                }
            }

            car.SpecialProperties?
                .Where(p => !(input.SpecialPropertiesIds ?? new()).Any(id => id == p.Id))
                .ToList()
                .ForEach(p => car.SpecialProperties.Remove(p));

            db.Update(car);
            db.SaveChanges();
        }

        public async Task<bool> Delete(int id)
        {
            var car = db.Cars.Find(id);

            car.IsDeleted = true;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public string OwnerId(int carId)
            => db.Cars.Find(carId).OwnerId;

        public IEnumerable<CarListingServiceModel> SortCars(
            IEnumerable<CarListingServiceModel> collection,
            CarSorting sorting = CarSorting.Price,
            SortingOrder sortingOrder = SortingOrder.Ascending)
        {
            var result = new List<CarListingServiceModel>();

            if (sortingOrder is SortingOrder.Ascending)
            {
                result = sorting switch
                {
                    CarSorting.Year => collection.OrderBy(x => x.Year).ThenBy(x => x.Price).ToList(),
                    CarSorting.Brand => collection.OrderBy(x => x.Brand).ThenBy(x => x.Price).ToList(),
                    CarSorting.Price => collection.OrderBy(x => x.Price).ToList(),
                    _ => result,
                };
            }
            else
            {
                result = sorting switch
                {
                    CarSorting.Year => collection.OrderByDescending(x => x.Year).ThenByDescending(x => x.Price).ToList(),
                    CarSorting.Brand => collection.OrderByDescending(x => x.Brand).ThenByDescending(x => x.Price).ToList(),
                    CarSorting.Price => collection.OrderByDescending(x => x.Price).ToList(),
                    _ => result,
                };
            }

            return result;
        }

        public AllCarsServiceModel All(
            int currentPage = 1,
            int carsPerPage = 20,
            string ownerId = null,
            CarSearchServiceModel searchModel = null,
            bool returnDeleted = false)
        {
            var query = searchModel is null ? this.db
                .Cars
                .AsQueryable() : this.Search(searchModel);

            if (!returnDeleted)
            {
                query = query.Where(x => x.IsDeleted == false);
            }

            if (!string.IsNullOrWhiteSpace(ownerId))
            {
                query = query.Where(x => x.OwnerId == ownerId);
            }

            var totalCars = query.Count();

            query = query
                .OrderByDescending(x => x.Id)
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage);

            return new AllCarsServiceModel()
            {
                TotalCars = totalCars,
                Cars = query
                        .Select(x => new CarListingServiceModel()
                        {
                            Id = x.Id,
                            Brand = x.Brand.Name,
                            Model = x.Model.Name,
                            Modification = x.Modification,
                            Price = x.Price,
                            TravelledDistance = x.TravelledDistance,
                            Year = x.ProduceYear,
                            HorsePower = x.HorsePower,
                            EngineType = x.EngineType.Name,
                            ImageUrl = x.Image.Url,
                            OwnerId = x.OwnerId,
                            OwnerName = x.Owner.UserName,
                            IsDeleted = x.IsDeleted
                        })
                        .ToList()
            };
        }

        private IQueryable<Car> Search(CarSearchServiceModel model)
        {
            var query = this.db
               .Cars
               .Where(x => !x.IsDeleted)
               .AsQueryable();

            query = model.BrandId is null ? query : query.Where(x => x.BrandId == model.BrandId);
            query = model.ModelId is null ? query : query.Where(x => x.ModelId == model.ModelId);
            query = model.PriceFrom is null ? query : query.Where(x => x.Price >= model.PriceFrom);
            query = model.PriceTo is null ? query : query.Where(x => x.Price <= model.PriceTo);
            query = model.YearFrom is null ? query : query.Where(x => x.ProduceYear >= model.YearFrom);
            query = model.YearTo is null ? query : query.Where(x => x.ProduceYear <= model.YearTo);
            query = model.HorsePowerFrom is null ? query : query.Where(x => x.HorsePower >= model.HorsePowerFrom);
            query = model.HorsePowerTo is null ? query : query.Where(x => x.HorsePower <= model.HorsePowerTo);
            query = model.EngineTypeId is null ? query : query.Where(x => x.EngineTypeId == model.EngineTypeId);
            query = model.TransmisionTypeId is null ? query : query.Where(x => x.TransmisionId == model.TransmisionTypeId);
            query = model.EuroStandardId is null ? query : query.Where(x => x.EuroStandardId == model.EuroStandardId);
            query = model.CoupeTypeId is null ? query : query.Where(x => x.CoupeTypeId == model.CoupeTypeId);
            query = model.MaxTravelledDistance is null ? query : query.Where(x => x.TravelledDistance <= model.MaxTravelledDistance);

            model.SafetyProperties = model.SafetyProperties?.Any() == false ? null : model.SafetyProperties;
            model.ComfortProperties = model.ComfortProperties?.Any() == false ? null : model.ComfortProperties;
            model.OtherProperties = model.OtherProperties?.Any() == false ? null : model.OtherProperties;
            model.ExteriorProperties = model.ExteriorProperties?.Any() == false ? null : model.ExteriorProperties;
            model.InteriorProperties = model.InteriorProperties?.Any() == false ? null : model.InteriorProperties;
            model.ProtectionProperties = model.ProtectionProperties?.Any() == false ? null : model.ProtectionProperties;
            model.SpecialProperties = model.SpecialProperties?.Any() == false ? null : model.SpecialProperties;

            query = model.SafetyProperties is null ? query : query.Where(x => x.SafetyProperties.Any(p => model.SafetyProperties.Any(m => m == p.Id)));
            query = model.ComfortProperties is null ? query : query.Where(x => x.ComfortProperties.Any(p => model.ComfortProperties.Any(m => m == p.Id)));
            query = model.OtherProperties is null ? query : query.Where(x => x.OtherProperties.Any(p => model.OtherProperties.Any(m => m == p.Id)));
            query = model.ExteriorProperties is null ? query : query.Where(x => x.ExteriorProperties.Any(p => model.ExteriorProperties.Any(m => m == p.Id)));
            query = model.InteriorProperties is null ? query : query.Where(x => x.InteriorProperties.Any(p => model.InteriorProperties.Any(m => m == p.Id)));
            query = model.ProtectionProperties is null ? query : query.Where(x => x.ProtectionProperties.Any(p => model.ProtectionProperties.Any(m => m == p.Id)));
            query = model.SpecialProperties is null ? query : query.Where(x => x.SpecialProperties.Any(p => model.SpecialProperties.Any(m => m == p.Id)));

            return query;
        }

        public async Task<int> CarsCountAsync()
            => await db.Cars.CountAsync(x => !x.IsDeleted);
    }
}
