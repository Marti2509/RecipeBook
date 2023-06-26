namespace RecipeBook.Common
{
    public static class ModelValidationConstants
    {
        public static class Recipe
        {
            public const int NameMaxLength = 40;
            public const int NameMinLength = 5;

            public const int ImageUrlMaxLength = 2048;

            public const int ServingsMinValue = 1;
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
        }
    }
}
