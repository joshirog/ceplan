using Domain.Commons.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class Role : IdentityRole<Guid>, IBaseIdentity
{
    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; }
    

    public ICollection<UserRole> UserRoles { get; set; }
}

