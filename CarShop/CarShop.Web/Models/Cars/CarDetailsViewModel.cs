using CarShop.Web.Services.Cars.Models;
using CarShop.Web.Services.Dealers;

namespace CarShop.Web.Models.Cars
{
    public class CarDetailsViewModel
    {
        public CarServiceModel Car { get; set; }

        public DealerServiceModel Dealer { get; set; }
    }
}
