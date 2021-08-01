namespace CarShop.Web.Controllers
{
    using CarShop.Services.Cars;
    using CarShop.Web.Models.Cars;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class CarsController : Controller
    {
        private readonly ICarsService carsService;

        public CarsController(ICarsService carsService)
        {
            this.carsService = carsService;
        }

        public IActionResult All()
        {
            var cars = carsService.GetCars(0, 10).Select(x => new CarViewModel
            {
                Id = x.Id,
                Brand = x.Brand,
                Model = x.Model,
                Modification = x.Modification,
                Price = x.Price,
                Description = x.Description,
                ProduceYear = x.ProduceYear,
                EngineType = x.EngineType,
                HorsePower = x.HorsePower,
                EuroStandard = x.EuroStandard,
                TransmisionType = x.TransmisionType,
                CoupeType = x.CoupeType,
                TravelledDistance = x.TravelledDistance,
                Color = x.Color,
                ImageUrl = x.ImageUrl
            });

            return this.View(cars);
        }
    }
}
