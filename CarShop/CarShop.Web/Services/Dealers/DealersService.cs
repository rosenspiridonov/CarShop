namespace CarShop.Web.Services.Dealers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CarShop.Services.Cars;
    using CarShop.Web.Data;
    using CarShop.Web.Data.Models;
    using CarShop.Web.Services.Cars.Models;

    using static WebConstants;

    public class DealersService : IDealersService
    {
        private readonly ApplicationDbContext db;
        private readonly ICarsService carsService;

        public DealersService(
            ApplicationDbContext db,
            ICarsService carsService)
        {
            this.db = db;
            this.carsService = carsService;
        }

        public DealerServiceModel GetById(string userId)
            => db.Users
                .Where(x => x.Id == userId)
                .Select(x => new DealerServiceModel
                {
                    Id = x.Id,
                    Name = x.UserName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                })
                .FirstOrDefault();

        public DealerServiceModel GetInfo(string userId)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);

            return new DealerServiceModel
            {
                Name = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
        }

        public bool OwnsCar(string userId, int carId)
        {
            var carOwnerId = carsService.GetCarViewModel(carId).OwnerId;
            return carOwnerId == userId;
        }

        public void ProcessRequest(string userId, string phoneNumber)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);

            user.PhoneNumber = phoneNumber;

            db.DealerRequests.Add(new DealerRequest
            {
                UserId = userId,
                IsAccepted = false,
                Pending = true,
            });

            db.SaveChanges();
        }
    }
}
