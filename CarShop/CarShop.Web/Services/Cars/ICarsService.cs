namespace CarShop.Services.Cars
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarShop.Web.Models.Sorting;
    using CarShop.Web.Models.Cars;
    using CarShop.Web.Services.Cars.Models;

    public interface ICarsService
    {
        IEnumerable<CarServiceModel> GetCars(int start, int count);

        Task<int> CreateCarAsync(string ownerId, CarInputModel input);

        Task<int> EditCarAsync(CarInputModel input); 

        CarFormData AllCarOptions();

        CarServiceModel GetCarViewModel(int id);

        CarInputModel CarInputModelInfo(int id);

        Task<bool> Delete(int id);

        string OwnerId(int carId);

        IEnumerable<CarListingServiceModel> SortCars(
            IEnumerable<CarListingServiceModel> collection,
            CarSorting sorting = CarSorting.Price,
            SortingOrder sortingOrder = SortingOrder.Ascending);

        AllCarsServiceModel All(int currentPage = 1, int carsPerPage = 20, string ownerId = null, CarSearchServiceModel searchModel = null);
    }
}
