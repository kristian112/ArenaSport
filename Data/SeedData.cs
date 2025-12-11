using ArenaSportProj.Data;
using ArenaSportProj.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArenaSportProj.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // 1) Seed Roles
        string[] roles = { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // 2) Seed Admin user
        var adminEmail = "admin@mail.com";
        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            var user = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
            await userManager.CreateAsync(user, "admin123");
            await userManager.AddToRoleAsync(user, "Admin");
        }
        else if (!await userManager.IsInRoleAsync(admin, "Admin"))
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        // 3) Seed Categories if empty
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Бокс" },
                new Category { Name = "Кикбокс" },
                new Category { Name = "ММА" },
                new Category { Name = "Муай Тай" },
                new Category { Name = "BJJ" },
                new Category { Name = "Крав Мага" }
            );
            context.SaveChanges();
        }

        // 4) Seed Products if empty
        if (!context.Products.Any())
        {
            var categories = context.Categories.ToList();

            context.Products.AddRange(
                new Product
                {
                    Name = "Boxing Gloves Pro",
                    Description = "Фиктивни премиум ръкавици за sparring",
                    Brand = "Venum",
                    Price = 120,
                    ImageUrl = "https://placehold.co/600x400",
                    CategoryId = categories.First(c => c.Name == "Бокс").Id
                },
                new Product
                {
                    Name = "MMA Shin Guards",
                    Description = "Леки и tough шин pads",
                    Brand = "Everlast",
                    Price = 80,
                    ImageUrl = "https://placehold.co/600x400",
                    CategoryId = categories.First(c => c.Name == "ММА").Id
                },
                new Product
                {
                    Name = "Kickboxing Punch Mitts",
                    Description = "Идеални лапи за тренировка",
                    Brand = "Twins",
                    Price = 95,
                    ImageUrl = "https://placehold.co/600x400",
                    CategoryId = categories.First(c => c.Name == "Кикбокс").Id
                }
            );
            
        }
        await context.SaveChangesAsync();
    }
}
