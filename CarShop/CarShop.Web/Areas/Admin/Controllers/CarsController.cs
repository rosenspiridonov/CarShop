namespace CarShop.Web.Areas.Admin.Controllers
{
    using System.Linq;

    using CarShop.Services.Cars;
    using CarShop.Web.Models.Cars;
    using CarShop.Web.Models.Sorting;
    using CarShop.Web.Services.Admin;

    using Microsoft.AspNetCore.Mvc;

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

        public IActionResult All(int page = 1, 
            CarSorting sorting = CarSorting.Year,
            SortingOrder order = SortingOrder.Ascending)
        {
            const int carsPerPage = 10;
            var result = carsService.All(page, carsPerPage, returnDeleted: true);

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

        public IActionResult Restore(int id)
        {
            adminService.RestoreCar(id);
            ;
            return RedirectToAction("All");
        }
    }
}
