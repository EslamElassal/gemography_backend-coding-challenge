using System.Security.Claims;

namespace BuisnessLayer.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetCurrentUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static long GetCurrentUserId(this ClaimsPrincipal user)
        {
            return long.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}