namespace CarShop.Services.Cars
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarShop.Web.Data.Models;
    using CarShop.Web.Models.Cars;
    using CarShop.Web.Services.Cars;

    public interface ICarsService
    {
        IEnumerable<CarServiceModel> GetCars(int start, int count);

        Task<int> CreateCarAsync(string ownerId, CarInputModel input);

        Task<int> EditCarAsync(CarInputModel input); 

        CarFormData AllCarOptions();

        CarServiceModel GetCarViewModel(int id);

        public CarInputModel CarInputModelInfo(int id);
    }
}
