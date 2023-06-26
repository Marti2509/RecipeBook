using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RecipeBook.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
