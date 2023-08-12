using NUnit.Framework.Internal.Execution;
using RecipeBook.Data;
using RecipeBook.Data.Models;

namespace RecipeBook.Core.Tests
{
    public static class SeedDatabase
    {
        public static ApplicationUser ChefUser;
        public static ApplicationUser User;
        public static Chef Chef;
        public static Category Category1;
        public static Category Category2;
        public static Recipe Recipe1;
        public static ApplicationUserRecipe ApplicationUserRecipe1;

        public async static Task SeedDb(RecipeBookDbContext dbContext)
        {
            ChefUser = new ApplicationUser()
            {
                UserName = "Pesho",
                NormalizedUserName = "PESHO",
                Email = "pesho@chefs.com",
                NormalizedEmail = "PESHO@CHEFS.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
                SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
                TwoFactorEnabled = false,
                FirstName = "Pesho",
                LastName = "Petrov",
                PhoneNumber = "0891234567",
            };

            User = new ApplicationUser()
            {
                UserName = "Mitko",
                NormalizedUserName = "Mitko",
                Email = "mitko@users.com",
                NormalizedEmail = "MITKO@USERS.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "ac67a9dc-9317-4460-b4cc-d57d75918077",
                SecurityStamp = "c6a9bcfb-a07c-4072-b997-5bcca90f8166",
                TwoFactorEnabled = false,
                FirstName = "Mitko",
                LastName = "Mitkov",
                PhoneNumber = "0897654321",
            };

            Chef = new Chef()
            {
                PhoneNumber = "0891234567",
                User = ChefUser,
                Name = "Peshoo"
            };

            Category1 = new Category()
            {
                Name = "Category1",
                IsActive = true
            };

            Category2 = new Category()
            {
                Name = "Category2",
                IsActive = true
            };

            Recipe1 = new Recipe()
            {
                Name = "Test",
                Servings = 2,
                Steps = "step1",
                Chef = Chef,
                CookingTime = 30,
                ImageUrl = "asdasdasdasdasd.com",
                IsActive = true,
                Products = "prod1",
                Category = Category1
            };

            ApplicationUserRecipe1 = new ApplicationUserRecipe
            {
                User = User,
                Recipe = Recipe1
            };

            dbContext.Users.Add(ChefUser);
            dbContext.Users.Add(User);
            dbContext.Chefs.Add(Chef);
            dbContext.Categories.Add(Category1);
            dbContext.Categories.Add(Category2);
            dbContext.Recipes.Add(Recipe1);
            dbContext.ApplicationUsersRecipes.Add(ApplicationUserRecipe1);

            await dbContext.SaveChangesAsync();
        }
    }
}
