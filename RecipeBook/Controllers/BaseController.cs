using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

using static RecipeBook.Common.ApplicationConstants;

namespace RecipeBook.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
        protected Guid GetUserGuidId()
        {
            string id = string.Empty;

            if (User != null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return Guid.Parse(id);
        }

        protected bool IsAdmin()
        {
            return User.IsInRole(AdminName);
        }
    }
}
