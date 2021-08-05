namespace CarShop.Web.Services.Dealers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarShop.Services.Cars;
    using CarShop.Web.Data;

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
    }
}
