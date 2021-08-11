namespace CarShop.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CarShop.Services.Cars;
    using CarShop.Web.Infrastructure;
    using CarShop.Web.Models.Sorting;
    using CarShop.Web.Models.Cars;
    using CarShop.Web.Models.Dealers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    public class DealersController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICarsService carsService;

        public DealersController(
            UserManager<IdentityUser> userManager,
            ICarsService carsService)
        {
            this.userManager = userManager;
            this.carsService = carsService;
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> BecomeAsync()
        {
            var user = await userManager.FindByIdAsync(this.User.GetId());
            var isDealer = await userManager.IsInRoleAsync(user, DealerRoleName);

            if (isDealer)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Become(DealerFormModel model)
        {
            var user = await userManager.FindByIdAsync(this.User.GetId());

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
            await userManager.AddToRoleAsync(user, DealerRoleName);

            return Redirect("/");
        }

        [Authorize(Roles = DealerRoleName + ", " + AdminRoleName)]
        public async Task<IActionResult> MyCars(
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

            var result = carsService.All(currentPage: page, carsPerPage: carsPerPage, ownerId: id);

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
    }
}
