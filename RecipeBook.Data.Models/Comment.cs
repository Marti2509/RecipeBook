﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static RecipeBook.Common.ModelValidationConstants.Comment;

namespace RecipeBook.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public int RecipeId { get; set; }

        [Required]
        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
    }
}