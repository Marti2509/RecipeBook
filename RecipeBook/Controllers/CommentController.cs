using Microsoft.AspNetCore.Mvc;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Models.Comment;
using RecipeBook.Core.Models.Recipe;
using RecipeBook.Core.Services;

using static RecipeBook.Common.NotificationMessagesConstants;

namespace RecipeBook.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService _commentService)
        {
            commentService = _commentService;
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            var model = new CommentFormModel()
            {
                RecipeId = id
            };

            ViewBag.Id = id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentFormModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await commentService.AddCommentAsync(model, GetUserGuidId(), ViewBag.Id);

                return RedirectToAction("Details", "Recipe", ViewBag.Id);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred, please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
