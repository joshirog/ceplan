using Application.Commons.Constants;
using Domain.Commons.Enums;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistences.Seeders;

public class RoleSeeder : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        var date = new DateTime(2025, 1, 1);
        
        builder.HasData(
            new Role
            {
                Id = Guid.Parse("28C879E4-46E1-4186-B052-05A74320AB01"), 
                Name = "Administrator",
                Status = Enum.GetName(typeof(RoleStatusEnum), RoleStatusEnum.Active),
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = Guid.Empty.ToString(),
                CreatedBy = GlobalConstant.DefaultUsername, 
                CreatedAt = date
            },
            new Role
            {
                Id = Guid.Parse("28C879E4-46E1-4186-B052-05A74320AB02"), 
                Name = "Client",
                Status = Enum.GetName(typeof(RoleStatusEnum), RoleStatusEnum.Active),
                NormalizedName = "CLIENT",
                ConcurrencyStamp = Guid.Empty.ToString(),
                CreatedBy = GlobalConstant.DefaultUsername, 
                CreatedAt = date
            }
        );
    }
}