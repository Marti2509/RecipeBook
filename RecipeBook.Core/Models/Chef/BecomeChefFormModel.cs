using System.ComponentModel.DataAnnotations;

using static RecipeBook.Common.ModelValidationConstants.Chef;

namespace RecipeBook.Core.Models.Chef
{
    public class BecomeChefFormModel
    {
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
