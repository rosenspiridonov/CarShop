using System.Linq;
using System.Threading.Tasks;

using CarShop.Services.Cars;
using CarShop.Web.Models.Cars;
using CarShop.Web.Models.Sorting;
using CarShop.Web.Services.Admin;

using Microsoft.AspNetCore.Mvc;

namespace CarShop.Web.Areas.Admin.Controllers
{
    public class CarsController : AdminController
    {
        private readonly ICarsService carsService;
        private readonly IAdminService adminService;

        public CarsController(
            ICarsService carsService,
            IAdminService adminService)
        {
            this.carsService = carsService;
            this.adminService = adminService;
        }

        public async Task<IActionResult> AllAsync(int page = 1, 
            CarSorting sorting = CarSorting.Price,
            SortingOrder order = SortingOrder.Ascending)
        {
            const int carsPerPage = 10;
            var result = await carsService.AllAsync(page, carsPerPage, returnDeleted: true);

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

        public async Task<IActionResult> RestoreAsync(int id)
        {
            await adminService.RestoreCarAsync(id);
            
            return RedirectToAction("All");
        }
    }
}
