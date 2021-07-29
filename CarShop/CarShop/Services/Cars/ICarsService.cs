namespace CarShop.Services.Cars
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarShop.Data.Models;
    using CarShop.Models.Cars;

    public interface ICarsService
    {
        IEnumerable<CarServiceModel> GetCars(int start, int count);

        Task<Car> CreateCarAsync(CarInputModel input);
    }
}
