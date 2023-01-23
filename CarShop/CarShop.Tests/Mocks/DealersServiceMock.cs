using System.Threading.Tasks;

using CarShop.Services.Cars;
using CarShop.Web.Data;
using CarShop.Web.Data.Models;
using CarShop.Web.Services.Dealers;

namespace CarShop.Tests.Mocks
{
    public class DealersServiceMock : DealersService
    {
        private readonly ApplicationDbContext db;

        public DealersServiceMock(ApplicationDbContext db, ICarsService carsService) 
            : base(db, carsService)
        {
            this.db = db;
        }

        public override async Task ProcessRequestAsync(string userId, string phoneNumber)
        {
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
