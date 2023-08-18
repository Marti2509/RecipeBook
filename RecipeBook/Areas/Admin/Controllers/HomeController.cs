using Microsoft.AspNetCore.Mvc;

namespace RecipeBook.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
