using System.Security.Claims;

namespace FrontendRestauranteMarisco.WebApp.Helpers
{
    public static class AuthHelper
    {
        public static string ObtenerToken(ClaimsPrincipal user)
        {
            return user.FindFirstValue("Token") ?? string.Empty;
        }

    }
}
