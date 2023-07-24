using System.Security.Claims;

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
    }
}
