using Microsoft.AspNetCore.Mvc;

using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.Category;

using static RecipeBook.Common.NotificationMessagesConstants;

namespace RecipeBook.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                if (!IsAdmin())
                {
                    return Unauthorized();
                }

                List<CategoryViewModel> models = await categoryService.GetAllCategoriesAsync();

                return View(models);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                if (!IsAdmin())
                {
                    return Unauthorized();
                }

                var model = new CategoryFormModel();

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryFormModel model)
        {
            try
            {
                if (!IsAdmin())
                {
                    return Unauthorized();
                }

                bool exists = await categoryService.CategoryExistsByNameAsync(model.Name);

                if (exists)
                {
                    ModelState.AddModelError(nameof(model.Name), "This category already exists!");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await categoryService.AddCategoryAsync(model);

                return RedirectToAction("All", "Category");
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
                if (!IsAdmin())
                {
                    return Unauthorized();
                }

                bool categoryExists = await categoryService.CategoryExistsByIdAsync(id);

                if (!categoryExists)
                {
                    TempData[ErrorMessage] = "Category not found!";

                    return RedirectToAction("All", "Categories");
                }

                var model = await categoryService.GetCategoryByIdAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryFormModel model)
        {
            try
            {
                if (!IsAdmin())
                {
                    return Unauthorized();
                }

                bool exists = await categoryService.CategoryExistsByNameAsync(model.Name);

                if (exists)
                {
                    ModelState.AddModelError(nameof(model.Name), "This category already exists!");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await categoryService.EditCategoryAsync(id, model);

                return RedirectToAction("All", "Category");
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
                if (!IsAdmin())
                {
                    return Unauthorized();
                }

                bool categoryExists = await categoryService.CategoryExistsByIdAsync(id);

                if (!categoryExists)
                {
                    TempData[ErrorMessage] = "Category not found!";

                    return RedirectToAction("All", "Categories");
                }

                var model = await categoryService.GetCategoryByIdAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CategoryFormModel model)
        {
            try
            {
                if (!IsAdmin())
                {
                    return Unauthorized();
                }

                await categoryService.DeleteCategory(id);

                return RedirectToAction("All", "Category");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
