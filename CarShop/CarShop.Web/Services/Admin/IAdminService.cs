namespace CarShop.Web.Services.Admin
{
    using System.Collections.Generic;

    using CarShop.Web.Services.Dealers;

    public interface IAdminService
    {
        IEnumerable<DealerServiceModel> DealersPendingRequests();

        void ApproveDealer(string userId);
    }
}
