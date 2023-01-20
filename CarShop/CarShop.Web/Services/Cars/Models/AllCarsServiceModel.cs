using System.Collections.Generic;

namespace CarShop.Web.Services.Cars.Models
{
    public class AllCarsServiceModel
    {
        public int TotalCars { get; set; }

        public IEnumerable<CarListingServiceModel> Cars { get; set; }
    }
}
