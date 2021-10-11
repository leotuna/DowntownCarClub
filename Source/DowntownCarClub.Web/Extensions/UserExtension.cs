using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace DowntownCarClub.Web.Extensions
{
    public static class UserExtension
    {
        public static Guid GetCurrentUserId(this Controller controller)
        {
            try
            {
                var userId = controller.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.Parse(userId);
            }
            catch
            {
                throw new Exception("Cannot find user id.");
            }
        }

        public static Guid GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.Parse(userId);
            }
            catch
            {
                throw new Exception("Cannot find user id.");
            }
        }

        public static bool UserIsAuthenticated(this Controller controller)
        {
            return controller.User.Identity.IsAuthenticated;
        }
    }
}
