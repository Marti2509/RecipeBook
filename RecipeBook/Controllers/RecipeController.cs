using Microsoft.AspNetCore.Mvc;

using static RecipeBook.Common.NotificationMessagesConstants;

using RecipeBook.Core.Models.Recipe;
using RecipeBook.Core.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace RecipeBook.Controllers
{
    public class RecipeController : BaseController
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery]AllRecipesQueryModel queryModel)
        {
            try
            {
                List<AllRecipesViewModel> recipes = await recipeService.AllRecipesAsync(queryModel);

                queryModel.Recipes = recipes;
                queryModel.Categories = await categoryService.GetAllCategoryNamesAsync();

                return View(queryModel);
            }
            catch (Exception ex)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                bool isChef = await chefService.ChefExistsByUserIdAsync(GetUserGuidId()!);

                if (!isChef)
                {
                    TempData[ErrorMessage] = "You should become Chef to Add Recipes!";

                    return RedirectToAction("Become", "Chef");
                }

                var categories = await categoryService.GetAllCategoriesAsync();

                AddRecipeFormModel model = new AddRecipeFormModel()
                {
                    Categories = categories
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRecipeFormModel model)
        {
            try
            {
                bool isChef = await chefService.ChefExistsByUserIdAsync(GetUserGuidId()!);

                if (!isChef)
                {
                    TempData[ErrorMessage] = "You should become Chef to Add Recipes!";

                    return RedirectToAction("Become", "Chef");
                }

                bool categoryExists = await categoryService.CategoryExistsByIdAsync(model.CategoryId);

                if (!categoryExists)
                {
                    ModelState.AddModelError(nameof(model.CategoryId), "Selected category doesn't exist!");
                }

                if (!ModelState.IsValid)
                {
                    var categories = await categoryService.GetAllCategoriesAsync();
                    model.Categories = categories;

                    return View(model);
                }

                var chefId = await chefService.GetChefIdByUserIdAsync(GetUserGuidId());

                await recipeService.AddRecipeAsync(model, chefId);

                return RedirectToAction("All", "Recipe");
            }
            catch (Exception ex)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
