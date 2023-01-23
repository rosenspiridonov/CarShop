using System.Security.Claims;

using static CarShop.Web.WebConstants;

namespace CarShop.Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdminRoleName);

        public static bool IsDealer(this ClaimsPrincipal user)
            => user.IsInRole(DealerRoleName);
    }
}
