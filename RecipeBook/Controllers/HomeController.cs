using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static RecipeBook.Common.ApplicationConstants;

namespace RecipeBook.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController()
        {
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.IsInRole(AdminName))
            {
                return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400)
            {
                return View("Error404");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            if (statusCode == 404)
            {
                return View("Error404");
            }

            return View();
        }
    }
}