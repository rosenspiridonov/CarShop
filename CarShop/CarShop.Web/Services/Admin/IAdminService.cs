using System.Collections.Generic;
using System.Threading.Tasks;

using CarShop.Web.Services.Dealers;

namespace CarShop.Web.Services.Admin
{
    public interface IAdminService
    {
        Task<IEnumerable<DealerServiceModel>> DealersPendingRequestsAsync();

        Task ApproveDealerAsync(string userId);

        Task RestoreCarAsync(int carId);
    }
}
