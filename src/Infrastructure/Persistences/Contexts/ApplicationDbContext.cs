using System.Reflection;
using Application.Commons.Interfaces;
using Domain.Commons.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Contexts;

public class ApplicationDbContext(
    DbContextOptions options,
    ICurrentUserService currentUserService,
    IDateTimeService dateTimeService)
    : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options),
        IApplicationDbContext
{
    public override DbSet<User> Users { get; set; }
    public override DbSet<Role> Roles { get; set; }
    public override DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {        
        base.OnModelCreating(builder);        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<IBaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = currentUserService.UserName;
                    entry.Entity.CreatedAt = dateTimeService.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedBy = currentUserService.UserName;
                    entry.Entity.UpdatedAt = dateTimeService.Now;
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
}