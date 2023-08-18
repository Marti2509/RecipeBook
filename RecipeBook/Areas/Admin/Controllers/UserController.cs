using Microsoft.AspNetCore.Mvc;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.User;

namespace RecipeBook.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("User/All")]
        public async Task<IActionResult> All()
        {
            IEnumerable<UserViewModel> viewModel =
                await userService.AllAsync();

            return View(viewModel);
        }
    }
}
