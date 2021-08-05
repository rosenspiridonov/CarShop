namespace CarShop.Web.Controllers
{
    using System.Threading.Tasks;

    using CarShop.Web.Infrastructure;
    using CarShop.Web.Models.Dealers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    public class DealersController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public DealersController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
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
    }
}
