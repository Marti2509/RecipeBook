using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using static RecipeBook.Common.ApplicationConstants;

namespace RecipeBook.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles = AdminName)]
    public class BaseAdminController : Controller
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
