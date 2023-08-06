using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Services;
using RecipeBook.Data;
using RecipeBook.Data.Models;
using Microsoft.AspNetCore.Identity;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RecipeBookDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = 
        builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
    options.Password.RequireLowercase = 
        builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
    options.Password.RequireUppercase = 
        builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
    options.Password.RequireDigit = 
        builder.Configuration.GetValue<bool>("Identity:Password:RequireDigit");
    options.Password.RequireNonAlphanumeric = 
        builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
    options.Password.RequiredLength = 
        builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
})
    .AddEntityFrameworkStores<RecipeBookDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IChefService, ChefService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();