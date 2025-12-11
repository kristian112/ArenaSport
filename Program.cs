using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ArenaSportProj.Data;
using ArenaSportProj.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using ArenaSportProj.Services;


var builder = WebApplication.CreateBuilder(args);

// ✅ 1. Добавяме MVC
builder.Services.AddControllersWithViews();

// ✅ 2. Добавяме Razor Pages (НЕОБХОДИМИ за Identity UI)
builder.Services.AddRazorPages();

// ✅ 3. SQL Server + EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ 4. Пълен Identity с Roles
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 4;

    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;

})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});


var app = builder.Build();

// ✅ 5. Middleware pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ✅ 6. Default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ✅ 7. Map-ваме Razor Pages (сега няма да гърми)
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await RealProductSeed.Seed(services);
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await SeedData.Initialize(services);
}



app.Run();
