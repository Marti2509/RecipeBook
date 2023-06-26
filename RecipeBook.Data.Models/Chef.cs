using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static RecipeBook.Common.ModelValidationConstants.Chef;

namespace RecipeBook.Data.Models
{
    public class Chef
    {
        public Chef()
        {
            this.Id = Guid.NewGuid();

            Recipes = new List<Recipe>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public ICollection<Recipe> Recipes { get; set; }
    }
}