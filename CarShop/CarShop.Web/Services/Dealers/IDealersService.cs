namespace CarShop.Web.Services.Dealers
{
    public interface IDealersService
    {
        DealerServiceModel GetInfo(string userId);

        bool OwnsCar(string userId, int carId);

        void ProcessRequest(string userId, string phoneNumber);

        DealerServiceModel GetById(string userId);
    }
}
