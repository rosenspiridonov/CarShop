using System.Linq;
using System.Threading.Tasks;

using CarShop.Services.Cars;
using CarShop.Web.Extensions;
using CarShop.Web.Models.Cars;
using CarShop.Web.Models.Sorting;
using CarShop.Web.Services.Cars.Models;
using CarShop.Web.Services.Dealers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static CarShop.Web.WebConstants;

namespace CarShop.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IDealersService dealersService;

        public CarsController(
            ICarsService carsService,
            IDealersService dealersService)
        {
            this.carsService = carsService;
            this.dealersService = dealersService;
        }

        [ResponseCache(Duration = 1800, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> AllAsync(
                int page = 1,
                CarSorting sorting = CarSorting.Price,
                SortingOrder order = SortingOrder.Ascending,
                CarSearchServiceModel searchModel = null)
        {
            const int carsPerPage = 10;

            var result = await carsService.AllAsync(currentPage: page, carsPerPage: carsPerPage, searchModel: searchModel);

            result.Cars = carsService.SortCars(result.Cars, sorting, order);

            var model = new AllCarsViewModel
            {
                CurrentPage = page,
                TotalCars = result.TotalCars,
                CarsPerPage = carsPerPage,
                Cars = result.Cars.ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> SearchAsync()
        {
            var model = new CarSearchServiceModel
            {
                ViewData = await carsService.AllCarOptionsAsync()
            };

            return View(model);
        }

        [Authorize(Roles = DealerRoleName + ", " + AdminRoleName)]
        public async Task<IActionResult> CreateAsync()
        {
            if (!User.IsDealer() && !User.IsAdmin())    
            {
                return this.RedirectToAction("Become", "Dealers");
            }

            var model = new CarFormModel()
            {
                Data = await carsService.AllCarOptionsAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = DealerRoleName + ", " + AdminRoleName)]
        public async Task<IActionResult> CreateAsync(CarFormModel input)
        {
            input.Data = await carsService.AllCarOptionsAsync();

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

        public async Task<IActionResult> DetailsAsync(int id)
        {
            var model = new CarDetailsViewModel
            {
                Car = await carsService.GetCarViewModelAsync(id)
            };

            if (model.Car.IsDeleted && User?.IsAdmin() == false)
            {
                return NotFound("Car does not exists!");
            }

            model.Dealer = await dealersService.GetInfoAsync(model.Car.OwnerId);

            return this.View(model);
        }

        [Authorize(Roles = DealerRoleName + ", " + AdminRoleName)]
        public async Task<IActionResult> EditAsync(int id)
        {
            var carModel = await carsService.CarInputModelInfoAsync(id);

            if (!await dealersService.OwnsCarAsync(this.User.GetId(), id) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var carFormModel = new CarFormModel
            {
                Car = carModel,
                Data = await carsService.AllCarOptionsAsync()
            };

            return this.View(carFormModel);
        }

        [HttpPost]
        [Authorize(Roles = DealerRoleName + ", " + AdminRoleName)]
        public async Task<IActionResult> EditAsync(CarFormModel input)
        {
            input.Data = await carsService.AllCarOptionsAsync();

            // Check for validation
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            if (!await dealersService.OwnsCarAsync(this.User.GetId(), input.Car.Id) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            await carsService.EditCarAsync(input.Car);

            return RedirectToAction("Details", "Cars", new { id = input.Car.Id });
        }

        [Authorize(Roles = DealerRoleName + ", " + AdminRoleName)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await dealersService.OwnsCarAsync(this.User.GetId(), id) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var isDeleted = await carsService.DeleteAsync(id);

            if (!isDeleted)
            {
                return BadRequest("An error occured while deleting the car.");
            }
           
            return RedirectToAction("Index", controllerName: "Home");
        }
    }
}
