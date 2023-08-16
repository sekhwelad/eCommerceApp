using System.Security.Claims;

namespace eShopper.Extensions
{
    public  static class ClaimsPrincipalExtensions
    {
        public static string RetreiveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}
