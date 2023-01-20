using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static CarShop.Web.Areas.Admin.AdminConstants;
using static CarShop.Web.WebConstants;

namespace CarShop.Web.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AdminRoleName)]
    public abstract class AdminController : Controller
    {
    }
}
