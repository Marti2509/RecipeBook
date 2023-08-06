using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Core.Models.User
{
    public class LoginFormModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public string? ReturnUrl { get; set; }
    }
}
