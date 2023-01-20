using System;

using CarShop.Web.Models;

using Microsoft.AspNetCore.Mvc;

namespace CarShop.Web.ViewComponents
{
    [ViewComponent(Name = "Pagination")]
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int currentPage, int totalCars, int carsPerPage, string conrollerName, string actionName)
        {
            var previousPage = currentPage - 1;

            if (previousPage < 1)
            {
                previousPage = 1;
            }

            var maxPage = (int)Math.Ceiling((double)totalCars / carsPerPage);

            var nextPage = currentPage + 1;

            if (nextPage > maxPage)
            {
                nextPage = maxPage;
            }

            var model = new PaginationViewModel()
            {
                PreviousPage = previousPage,
                NextPage = nextPage,
                ControllerName = conrollerName,
                ActionName = actionName,
            };

            return View(model);
        }
    }
}
