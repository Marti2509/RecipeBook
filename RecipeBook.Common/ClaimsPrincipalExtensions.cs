using System.Security.Claims;

using static RecipeBook.Common.ApplicationConstants;

namespace RecipeBook.Common
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetGuidId(this ClaimsPrincipal user)
        {
            string id = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return null;
            }

            return Guid.Parse(id);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminName);
        }
    }
}
