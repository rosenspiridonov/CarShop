using System.Collections.Generic;
using System.Threading.Tasks;

using CarShop.Web.Models.Sorting;
using CarShop.Web.Models.Cars;
using CarShop.Web.Services.Cars.Models;

namespace CarShop.Services.Cars
{
    public interface ICarsService
    {
        Task<int> CarsCountAsync();

        Task<AllCarsServiceModel> AllAsync(
            int currentPage = 1,
            int carsPerPage = 20,
            string ownerId = null,
            CarSearchServiceModel searchModel = null,
            bool returnDeleted = false);

        Task<CarFormData> AllCarOptionsAsync();

        Task<CarServiceModel> GetCarViewModelAsync(int id);

        Task<CarInputModel> CarInputModelInfoAsync(int id);

        IEnumerable<CarListingServiceModel> SortCars(
            IEnumerable<CarListingServiceModel> collection,
            CarSorting sorting = CarSorting.Price,
            SortingOrder sortingOrder = SortingOrder.Ascending);

        Task<int> CreateCarAsync(string ownerId, CarInputModel input);

        Task EditCarAsync(CarInputModel input); 

        Task<bool> DeleteAsync(int id);

    }
}
