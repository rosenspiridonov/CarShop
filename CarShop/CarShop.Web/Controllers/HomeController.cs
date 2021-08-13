namespace CarShop.Web.Controllers
{
    using CarShop.Services.Cars;
    using CarShop.Web.Models.Home;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly ICarsService carsService;

        public HomeController(ICarsService carsService)
        {
            this.carsService = carsService;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                CarsCount = carsService.CarsCount()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
