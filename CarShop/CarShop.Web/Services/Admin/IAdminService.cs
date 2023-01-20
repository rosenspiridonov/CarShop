using System.Collections.Generic;

using CarShop.Web.Services.Dealers;

namespace CarShop.Web.Services.Admin
{
    public interface IAdminService
    {
        IEnumerable<DealerServiceModel> DealersPendingRequests();

        void ApproveDealer(string userId);

        void RestoreCar(int carId);
    }
}
