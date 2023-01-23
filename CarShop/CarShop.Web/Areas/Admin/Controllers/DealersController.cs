using System.Threading.Tasks;

using CarShop.Web.Services.Admin;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using static CarShop.Web.WebConstants;

namespace CarShop.Web.Areas.Admin.Controllers
{
    public class DealersController : AdminController
    {
        private readonly IAdminService adminService;
        private readonly UserManager<IdentityUser> userManager;

        public DealersController(IAdminService adminService, UserManager<IdentityUser> userManager)
        {
            this.adminService = adminService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> RequestsAsync()
        {
            var requests = await adminService.DealersPendingRequestsAsync();

            return View(requests);
        }

        [HttpPost]
        public async Task<IActionResult> RequestsAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var isDealer = await userManager.IsInRoleAsync(user, DealerRoleName);

            if (isDealer)
            {
                return Redirect("/");
            }

            await userManager.AddToRoleAsync(user, DealerRoleName);
            await adminService.ApproveDealerAsync(userId);

            var requests = await adminService.DealersPendingRequestsAsync();

            return View(requests);  
            }
    }
}
