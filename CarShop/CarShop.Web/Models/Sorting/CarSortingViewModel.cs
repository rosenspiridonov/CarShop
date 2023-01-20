using System.ComponentModel.DataAnnotations;

namespace CarShop.Web.Models.Sorting
{
    public class CarSortingViewModel
    {
        [Display(Name = "Sort by:")]
        public CarSorting Sorting { get; set; }

        [Display(Name = "Order by:")]
        public SortingOrder Order { get; set; }
    }
}
