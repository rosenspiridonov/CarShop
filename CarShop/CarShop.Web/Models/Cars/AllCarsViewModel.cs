namespace CarShop.Web.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarShop.Web.Models.Sorting;
    using CarShop.Web.Services.Cars.Models;

    public class AllCarsViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalCars { get; set; }

        public int CarsPerPage { get; set; }

        public IList<CarListingServiceModel> Cars { get; set; }

        public CarSortingViewModel SortingModel { get; set; }
    }
}
