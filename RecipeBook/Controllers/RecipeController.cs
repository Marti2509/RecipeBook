﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.Recipe;

using static RecipeBook.Common.NotificationMessagesConstants;
using static RecipeBook.Common.ApplicationConstants;

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
                if (User.IsInRole(AdminName))
                {
                    return RedirectToAction("All", "Recipe", new { Area = AdminAreaName });
                }

                List<AllRecipesViewModel> recipes = await recipeService.AllRecipesAsync(queryModel);

                queryModel.Recipes = recipes;
                queryModel.Categories = await categoryService.GetAllCategoryNamesAsync();


                return View(queryModel);
            }
            catch (Exception)
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

                RecipeFormModel model = new RecipeFormModel()
                {
                    Categories = categories
                };

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(RecipeFormModel model)
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
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                bool recipeExists = await recipeService.ExistsByIdAsync(id);

                if(!recipeExists)
                {
                    TempData[ErrorMessage] = "Recipe with the provided id does not exists!";

                    return RedirectToAction("All", "Recipe");
                }

                var model = await recipeService.RecipeDetailsAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                bool recipeExists = await recipeService.ExistsByIdAsync(id);

                if (!recipeExists)
                {
                    TempData[ErrorMessage] = "Recipe with the provided id does not exists!";

                    return RedirectToAction("All", "Recipe");
                }

                if (!IsAdmin())
                {
                    bool isChef = await chefService.ChefExistsByUserIdAsync(GetUserGuidId()!);

                    if (!isChef)
                    {
                        TempData[ErrorMessage] = "You should become Chef to Edit Recipes!";

                        return RedirectToAction("Become", "Chef");
                    }

                    var chefId = await chefService.GetChefIdByUserIdAsync(GetUserGuidId());

                    bool isRecipesChef = await recipeService.IsChefWithIdOwnerOfRecipeWithIdAsync(chefId, id);

                    if (!isRecipesChef)
                    {
                        TempData[ErrorMessage] = "You should be the Chef of the Recipe to Edit it!";

                        return RedirectToAction("All", "Recipe");
                    }
                }

                RecipeFormModel model = await recipeService.RecipeForEditByIdAsync(id);

                model.Categories = await categoryService.GetAllCategoriesAsync();

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RecipeFormModel model)
        {
            try
            {
                bool recipeExists = await recipeService.ExistsByIdAsync(id);

                if (!recipeExists)
                {
                    TempData[ErrorMessage] = "Recipe with the provided id does not exists!";

                    return RedirectToAction("All", "Recipe");
                }

                if (!IsAdmin())
                {
                    bool isChef = await chefService.ChefExistsByUserIdAsync(GetUserGuidId()!);

                    if (!isChef)
                    {
                        TempData[ErrorMessage] = "You should become Chef to Edit Recipes!";

                        return RedirectToAction("Become", "Chef");
                    }

                    var chefId = await chefService.GetChefIdByUserIdAsync(GetUserGuidId());

                    bool isRecipesChef = await recipeService.IsChefWithIdOwnerOfRecipeWithIdAsync(chefId, id);

                    if (!isRecipesChef)
                    {
                        TempData[ErrorMessage] = "You should be the Chef of the Recipe to Edit it!";

                        return RedirectToAction("All", "Recipe");
                    }
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

                await recipeService.EditRecipeAsync(id, model);

                return RedirectToAction("Details", "Recipe", new { id });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            try
            {
                bool isChef = await chefService.ChefExistsByUserIdAsync(GetUserGuidId()!);

                if (!isChef)
                {
                    TempData[ErrorMessage] = "You should become Chef to see your recipes!";

                    return RedirectToAction("Become", "Chef");
                }

                var chefId = await chefService.GetChefIdByUserIdAsync(GetUserGuidId());

                var recipes = await recipeService.MineRecipesAsync(chefId);

                return View(recipes);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Saved()
        {
            try
            {
                var recipes = await recipeService.SavedRecipesAsync(GetUserGuidId());

                return View(recipes);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(int id)
        {
            try
            {
                await recipeService.SaveRecipe(GetUserGuidId(), id);

                return RedirectToAction("Saved", "Recipe");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Unsave(int id)
        {
            try
            {
                await recipeService.UnsaveRecipe(GetUserGuidId(), id);

                return RedirectToAction("Saved", "Recipe");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool recipeExists = await recipeService.ExistsByIdAsync(id);

                if (!recipeExists)
                {
                    TempData[ErrorMessage] = "Recipe with the provided id does not exists!";

                    return RedirectToAction("All", "Recipe");
                }

                if (!IsAdmin())
                {
                    bool isChef = await chefService.ChefExistsByUserIdAsync(GetUserGuidId()!);

                    if (!isChef)
                    {
                        TempData[ErrorMessage] = "You should become Chef to Edit Recipes!";

                        return RedirectToAction("Become", "Chef");
                    }

                    var chefId = await chefService.GetChefIdByUserIdAsync(GetUserGuidId());

                    bool isRecipesChef = await recipeService.IsChefWithIdOwnerOfRecipeWithIdAsync(chefId, id);

                    if (!isRecipesChef)
                    {
                        TempData[ErrorMessage] = "You should be the Chef of the Recipe to Edit it!";

                        return RedirectToAction("All", "Recipe");
                    }
                }

                var model = await recipeService.GetRecipeForDeleteAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, DeleteRecipeViewModel model)
        {
            try
            {
                bool recipeExists = await recipeService.ExistsByIdAsync(id);

                if (!recipeExists)
                {
                    TempData[ErrorMessage] = "Recipe with the provided id does not exists!";

                    return RedirectToAction("All", "Recipe");
                }

                if (!IsAdmin())
                {
                    bool isChef = await chefService.ChefExistsByUserIdAsync(GetUserGuidId()!);

                    if (!isChef)
                    {
                        TempData[ErrorMessage] = "You should become Chef to Edit Recipes!";

                        return RedirectToAction("Become", "Chef");
                    }

                    var chefId = await chefService.GetChefIdByUserIdAsync(GetUserGuidId());

                    bool isRecipesChef = await recipeService.IsChefWithIdOwnerOfRecipeWithIdAsync(chefId, id);

                    if (!isRecipesChef)
                    {
                        TempData[ErrorMessage] = "You should be the Chef of the Recipe to Edit it!";

                        return RedirectToAction("All", "Recipe");
                    }
                }

                await recipeService.DeleteRecipeAsync(id);

                return RedirectToAction("Mine", "Recipe");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
