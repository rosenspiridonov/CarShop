namespace CarShop.Web.Services.Admin
{
    using System.Collections.Generic;
    using System.Linq;

    using CarShop.Web.Data;
    using CarShop.Web.Services.Dealers;

    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext db;
        private readonly IDealersService dealersService;

        public AdminService(ApplicationDbContext db, IDealersService dealersService)
        {
            this.db = db;
            this.dealersService = dealersService;
        }

        public virtual void ApproveDealer(string userId)
        {
            var request = db.DealerRequests.FirstOrDefault(x => x.UserId == userId);

            request.Pending = false;
            request.IsAccepted = true;

            db.SaveChanges();
        }

        public virtual IEnumerable<DealerServiceModel> DealersPendingRequests()
        {
            var result = db
                .DealerRequests
                .Where(x => x.Pending && !x.IsAccepted)
                .Select(x => new DealerServiceModel
                {
                    Id = x.UserId,
                })
                .ToList();

            foreach (var request in result)
            {
                var dealerInfo = dealersService.GetById(request.Id);
                request.Name = dealerInfo.Name;
                request.PhoneNumber = dealerInfo.PhoneNumber;
                request.Email = dealerInfo.Email;
            }

            return result;
        }
    }
}
