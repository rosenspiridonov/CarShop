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

        public override void ProcessRequest(string userId, string phoneNumber)
        {
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
