using System.Security.Claims;

namespace Advanced_Web_Application
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        public static bool HasRole(this ClaimsPrincipal user,string Role)
        {
            return user.IsInRole(Role);
        }
    }
}
