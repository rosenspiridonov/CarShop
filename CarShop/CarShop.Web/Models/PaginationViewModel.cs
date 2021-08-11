namespace CarShop.Web.Models
{
    public class PaginationViewModel
    {
        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }
    }
}
