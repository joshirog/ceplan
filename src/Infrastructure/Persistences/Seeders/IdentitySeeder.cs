using Application.Commons.Constants;
using Domain.Commons.Enums;
using Domain.Entities;
using Infrastructure.Persistences.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistences.Seeders;

public static class IdentitySeeder
{
    public static async void Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        if (await userManager.FindByNameAsync("12345678") is not null) 
            return;
        
        var user = new User
        {
            Id = Guid.Parse("D44EAF5B-6B20-4099-BF46-A77F239378DF"),
            Name = "Administrator",
            LastName = "Administrator",
            FirstName = "Administrator",
            DocumentType = "DNI",
            UserName = "12345678",
            Email = "administrator@ceplan.com",
            EmailConfirmed = false,
            PhoneNumber = "999999999",
            Status = Enum.GetName(typeof(UserStatusEnum), UserStatusEnum.Active),
            TwoFactorEnabled = false,
            CreatedBy = GlobalConstant.DefaultUsername,
            CreatedAt = DateTime.Now
        };

        await userManager.CreateAsync(user, "Admin2025$$");
        
        await userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleEnum), RoleEnum.Administrator)!);
        
        await context.SaveChangesAsync();
    }       
}