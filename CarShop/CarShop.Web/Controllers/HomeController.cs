using System.Threading.Tasks;

using CarShop.Services.Cars;
using CarShop.Web.Models.Home;

using Microsoft.AspNetCore.Mvc;

namespace CarShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarsService carsService;

        public HomeController(ICarsService carsService)
        {
            this.carsService = carsService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                CarsCount = await carsService.CarsCountAsync()
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
