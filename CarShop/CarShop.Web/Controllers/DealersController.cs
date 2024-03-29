﻿using System.Linq;
using System.Threading.Tasks;

using CarShop.Services.Cars;
using CarShop.Web.Models.Sorting;
using CarShop.Web.Models.Cars;
using CarShop.Web.Models.Dealers;
using CarShop.Web.Services.Dealers;
using CarShop.Web.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using static CarShop.Web.WebConstants;

namespace CarShop.Web.Controllers
{
    public class DealersController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICarsService carsService;
        private readonly IDealersService dealersService;

        public DealersController(
            UserManager<IdentityUser> userManager,
            ICarsService carsService,
            IDealersService dealersService)
        {
            this.userManager = userManager;
            this.carsService = carsService;
            this.dealersService = dealersService;
        }

        [Authorize(Roles = UserRoleName)]
        public IActionResult Become()
        {
            if (this.User.IsDealer() || this.User.IsAdmin())
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = UserRoleName)]
        public async Task<IActionResult> BecomeAsync(DealerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await dealersService.ProcessRequestAsync(this.User.GetId(), model.PhoneNumber);

            return RedirectToAction("ThankYou", "Dealers");
        }

        [Authorize(Roles = DealerRoleName + ", " + AdminRoleName)]
        public async Task<IActionResult> MyCarsAsync(
                string id, 
                int page = 1, 
                CarSorting sort = CarSorting.Year, 
                SortingOrder order = SortingOrder.Ascending)
        {
            const int carsPerPage = 12;

            if (this.User.GetId() != id && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var currentUser = await userManager.FindByIdAsync(id);

            if (currentUser is null)
            {
                return NotFound();
            }

            var result = await carsService.AllAsync(currentPage: page, carsPerPage: carsPerPage, ownerId: id, returnDeleted: false);

            result.Cars = carsService.SortCars(result.Cars, sort, order);

            var model = new AllCarsViewModel()
            {
                Cars = result.Cars.ToList(),
                TotalCars = result.TotalCars,
                CarsPerPage = carsPerPage,
                CurrentPage = page,
                SortingModel = new CarSortingViewModel
                {
                    Sorting = sort,
                    Order = order
                }
            };

            return View(model);
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
