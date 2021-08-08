namespace CarShop.Web.Controllers
{
    using CarShop.Services.Cars;
    using CarShop.Web.Data.Models;
    using CarShop.Web.Infrastructure;
    using CarShop.Web.Models.Cars;
    using CarShop.Web.Services.Dealers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using static WebConstants;

    public class CarsController : Controller
    {
        private readonly ICarsService carsService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IDealersService dealersService;

        public CarsController(
            ICarsService carsService, 
            UserManager<IdentityUser> userManager,
            IDealersService dealersService)
        {
            this.carsService = carsService;
            this.userManager = userManager;
            this.dealersService = dealersService;
        }

        public IActionResult All()
        {
            var cars = carsService.GetCars(0, 10);

            return this.View(cars);
        }

        [Authorize(Roles = DealerRoleName + ", " + AdminRoleName)]
        public IActionResult Create()
        {
            var model = new CarFormModel()
            {
                Data = carsService.AllCarOptions(),
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = DealerRoleName + ", " + AdminRoleName)]
        public async Task<IActionResult> Create(CarFormModel input)
        {
            input.Data = carsService.AllCarOptions();

            // Check for validation
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            // Create car
            var userId = this.User.GetId();
            var carId = await carsService.CreateCarAsync(userId, input.Car);

            return RedirectToAction("Details", "Cars", new { id = carId });
        }

        public IActionResult Details(int id)
        {
            var model = new CarDetailsViewModel
            {
                Car = carsService.GetCarViewModel(id)
            };

            model.Dealer = dealersService.GetInfo(model.Car.OwnerId);

            return this.View(model);
        }

        [Authorize(Roles = DealerRoleName + ", " + AdminRoleName)]
        public IActionResult Edit(int id)
        {
            var carModel = carsService.CarInputModelInfo(id);

            if (!dealersService.OwnsCar(this.User.GetId(), id) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var carFormModel = new CarFormModel
            {
                Car = carModel,
                Data = carsService.AllCarOptions()
            };

            return this.View(carFormModel);
        }

        [HttpPost]
        [Authorize(Roles = DealerRoleName + ", " + AdminRoleName)]
        public async Task<IActionResult> Edit(CarFormModel input)
        {
            input.Data = carsService.AllCarOptions();

            // Check for validation
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            if (!dealersService.OwnsCar(this.User.GetId(), input.Car.Id) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var carId = await carsService.EditCarAsync(input.Car);

            return RedirectToAction("Details", "Cars", new { id = carId });
        }

        [Authorize(Roles = DealerRoleName + ", " + AdminRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!dealersService.OwnsCar(this.User.GetId(), id) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var ownerId = carsService.OwnerId(id);

            var isDeleted = await carsService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest("An error occured while deleting the car.");
            }

            return RedirectToAction("Cars", "Dealers", new { id = ownerId });
        }
    }
}
