﻿namespace RecipeBook.Common
{
    public static class ModelValidationConstants
    {
        public static class Recipe
        {
            public const int NameMaxLength = 40;
            public const int NameMinLength = 5;

            public const int ImageUrlMaxLength = 2048;

            public const int ServingsMinValue = 1;

            public const int CookingTimeMinValue = 1;
        }

        public static class Category
        {
            public const int NameMaxLength = 25;
            public const int NameMinLength = 4;
        }

        public static class Comment
        {
            public const int TextMaxLength = 200;
            public const int TextMinLength = 5;
        }

        public static class Chef
        {
            public const int NameMaxLength = 15;
            public const int NameMinLength = 3;

            public const int PhoneNumberMaxLength = 12;
            public const int PhoneNumberMinLength = 10;
        }

        public static class User
        {
            public const int FirstNameMaxLength = 15;
            public const int FirstNameMinLength = 3;

            public const int LastNameMaxLength = 15;
            public const int LastNameMinLength = 3;

            public const int PasswordMaxLength = 100;
            public const int PasswordMinLength = 6;
        }
    }
}
