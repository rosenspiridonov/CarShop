namespace CarShop.Web.Controllers
{
    using CarShop.Services.Cars;
    using CarShop.Web.Data.Models;
    using CarShop.Web.Models.Cars;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class CarsController : Controller
    {
        private readonly ICarsService carsService;
        private readonly UserManager<IdentityUser> userManager;

        public CarsController(ICarsService carsService, UserManager<IdentityUser> userManager)
        {
            this.carsService = carsService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var cars = carsService.GetCars(0, 10);

            return this.View(cars);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public IActionResult Create()
        {
            var model = new CreateCarInputModel()
            {
                Data = carsService.GetCarEntities(),
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Dealer")]
        public async Task<IActionResult> Create(CreateCarInputModel input)
        {
            input.Data = carsService.GetCarEntities();

            // Check for validation
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            // Create car
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var car = await carsService.CreateCarAsync(userId, input.Car);

            return RedirectToAction("All", "Cars");
        }

        public IActionResult Car(int id)
        {
            var car = carsService.GetCar(id);

            return this.View(car);
        }
    }
}
