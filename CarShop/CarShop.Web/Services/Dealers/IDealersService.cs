using System.Threading.Tasks;

namespace CarShop.Web.Services.Dealers
{
    public interface IDealersService
    {
        Task<DealerServiceModel> GetInfoAsync(string userId);

        Task<bool> OwnsCarAsync(string userId, int carId);

        Task ProcessRequestAsync(string userId, string phoneNumber);

        Task<DealerServiceModel> GetByIdAsync(string userId);
    }
}
