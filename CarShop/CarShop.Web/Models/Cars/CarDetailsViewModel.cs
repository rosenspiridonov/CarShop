namespace CarShop.Web.Models.Cars
{
    using CarShop.Web.Services.Cars;
    using CarShop.Web.Services.Dealers;

    public class CarDetailsViewModel
    {
        public CarServiceModel Car { get; set; }

        public DealerServiceModel Dealer { get; set; }
    }
}
