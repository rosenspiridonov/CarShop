namespace CarShop.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using CarShop.Web.Services.Admin;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    public class DealersController : AdminController
    {
        private readonly IAdminService adminService;
        private readonly UserManager<IdentityUser> userManager;

        public DealersController(IAdminService adminService, UserManager<IdentityUser> userManager)
        {
            this.adminService = adminService;
            this.userManager = userManager;
        }

        public IActionResult Requests()
        {
            var requests = adminService.DealersPendingRequests();

            return View(requests);
        }

        [HttpPost]
        public async Task<IActionResult> Requests(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var isDealer = await userManager.IsInRoleAsync(user, DealerRoleName);

            if (isDealer)
            {
                return Redirect("/");
            }

            await userManager.AddToRoleAsync(user, DealerRoleName);
            adminService.ApproveDealer(userId);

            var requests = adminService.DealersPendingRequests();

            return View(requests);  
            }
    }
}
