using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    }
}
