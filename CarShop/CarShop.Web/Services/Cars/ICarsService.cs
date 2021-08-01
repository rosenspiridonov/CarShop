namespace CarShop.Services.Cars
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarShop.Web.Data.Models;
    using CarShop.Web.Models.Cars;

    public interface ICarsService
    {
        IEnumerable<CarServiceModel> GetCars(int start, int count);

        Task<Car> CreateCarAsync(CarInputModel input);

        CreateCarViewData GetCarEntities();
    }
}
