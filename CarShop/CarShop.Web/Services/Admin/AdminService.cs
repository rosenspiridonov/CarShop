using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarShop.Web.Data;
using CarShop.Web.Services.Dealers;

using Microsoft.EntityFrameworkCore;

namespace CarShop.Web.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext db;
        private readonly IDealersService dealersService;

        public AdminService(ApplicationDbContext db, IDealersService dealersService)
        {
            this.db = db;
            this.dealersService = dealersService;
        }

        public virtual async Task ApproveDealerAsync(string userId)
        {
            var request = await db.DealerRequests.FirstOrDefaultAsync(x => x.UserId == userId);

            request.Pending = false;
            request.IsAccepted = true;

            await db.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<DealerServiceModel>> DealersPendingRequestsAsync()
        {
            var result = await db
                .DealerRequests
                .Where(x => x.Pending && !x.IsAccepted)
                .Select(x => new DealerServiceModel
                {
                    Id = x.UserId,
                })
                .ToListAsync();

            foreach (var request in result)
            {
                var dealerInfo = await dealersService.GetByIdAsync(request.Id);
                request.Name = dealerInfo.Name;
                request.PhoneNumber = dealerInfo.PhoneNumber;
                request.Email = dealerInfo.Email;
            }

            return result;
        }

        public async Task RestoreCarAsync(int carId)
        {
            var car = await db.Cars.FindAsync(carId);
            car.IsDeleted = false;
            await db.SaveChangesAsync();
        }
    }
}
