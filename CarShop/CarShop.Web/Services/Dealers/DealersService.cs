using System.Linq;
using System.Threading.Tasks;

using CarShop.Services.Cars;
using CarShop.Web.Data;
using CarShop.Web.Data.Models;

using Microsoft.EntityFrameworkCore;


namespace CarShop.Web.Services.Dealers
{
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

        public async Task<DealerServiceModel> GetByIdAsync(string userId)
            => await db.Users
                .Where(x => x.Id == userId)
                .Select(x => new DealerServiceModel
                {
                    Id = x.Id,
                    Name = x.UserName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                })
                .FirstOrDefaultAsync();

        public async Task<DealerServiceModel> GetInfoAsync(string userId)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);

            return new DealerServiceModel
            {
                Name = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<bool> OwnsCarAsync(string userId, int carId)
        {
            var carOwnerId = (await carsService.GetCarViewModelAsync(carId)).OwnerId;
            return carOwnerId == userId;
        }

        public virtual async Task ProcessRequestAsync(string userId, string phoneNumber)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);

            user.PhoneNumber = phoneNumber;

            await db.DealerRequests.AddAsync(new DealerRequest
            {
                UserId = userId,
                IsAccepted = false,
                Pending = true,
            });

            await db.SaveChangesAsync();
        }
    }
}
