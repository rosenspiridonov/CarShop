namespace CarShop.Web.Services.Cars.Models
{
    using System.Collections.Generic;

    public class AllCarsServiceModel
    {
        public int TotalCars { get; set; }

        public IEnumerable<CarListingServiceModel> Cars { get; set; }
    }
}
