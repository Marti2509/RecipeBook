using Microsoft.AspNetCore.Mvc;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.Recipe;

namespace RecipeBook.Areas.Admin.Controllers
{
    public class RecipeController : BaseAdminController
    {
        private readonly IRecipeService recipeService;
        private readonly ICategoryService categoryService;
        private readonly IChefService chefService;

        public RecipeController(IRecipeService _recipeService, ICategoryService _categoryService, IChefService _chefService)
        {
            recipeService = _recipeService;
            categoryService = _categoryService;
            chefService = _chefService;
        }

        public async Task<IActionResult> All()
        {
            List<AllRecipesViewModel> viewModel = await recipeService.AllRecipesAsync();

            return View(viewModel);
        }
    }
}
