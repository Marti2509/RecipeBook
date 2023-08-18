using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Contracts;
using RecipeBook.Core.Services;
using RecipeBook.Data;
using RecipeBook.Data.Models;
using Microsoft.AspNetCore.Identity;
using RecipeBook.Core.Extensions;
using static RecipeBook.Common.ApplicationConstants;

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
.AddRoles<IdentityRole<Guid>>()
.AddEntityFrameworkStores<RecipeBookDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IChefService, ChefService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.LoginPath = "/User/Login";
    cfg.AccessDeniedPath = "/Home/Error/401";
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.SeedAdmin(AdminEmail);

app.UseEndpoints(config =>
{
    config.MapControllerRoute(
        name: "areas",
        pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    config.MapControllerRoute(
        name: "ProtectingUrlRoute",
        pattern: "/{controller}/{action}/{id}/{information}",
        defaults: new { Controller = "Home", Action = "Index" });
    config.MapDefaultControllerRoute();
    config.MapRazorPages();
});

app.Run();