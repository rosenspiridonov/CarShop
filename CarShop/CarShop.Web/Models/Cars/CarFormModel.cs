namespace CarShop.Web.Models.Cars
{
    using CarShop.Web.Services.Cars.Models;

    public class CarFormModel
    {
        public CarFormData Data { get; set; }

        public CarInputModel Car { get; set; }
    }
}
