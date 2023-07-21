using Microsoft.AspNetCore.Mvc;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.Chef;
using static RecipeBook.Common.NotificationMessagesConstants;


namespace RecipeBook.Controllers
{
    public class ChefController : BaseController
    {
        private readonly IChefService chefService;

        public ChefController(IChefService _chefService)
        {
            chefService = _chefService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            Guid userId = GetUserGuidId();
            bool isChef = await chefService.ChefExistsByUserIdAsync(userId);
            if (isChef)
            {
                TempData[ErrorMessage] = "You are already a chef!";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeChefFormModel model)
        {
            Guid userId = GetUserGuidId();
            bool isChef = await chefService.ChefExistsByUserIdAsync(userId);
            if (isChef)
            {
                TempData[ErrorMessage] = "You are already a chef!";

                return RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberTaken =
                await chefService.ChefExistsByPhoneNumberAsync(model.PhoneNumber);
            if (isPhoneNumberTaken)
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Chef with the provided phone number already exists!");
            }

            bool isNameTaken =
                await chefService.ChefExistsByNameAsync(model.Name);
            if (isNameTaken)
            {
                ModelState.AddModelError(nameof(model.Name), "Chef with the provided name already exists!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await chefService.Create(userId, model.Name, model.PhoneNumber);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] =
                    "Unexpected error occurred while registering you as a chef! Please try again later or contact administrator.";

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("All", "Recipe");
        }
    }
}
