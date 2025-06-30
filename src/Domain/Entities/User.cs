using Domain.Commons.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>, IBaseIdentity
{
    public string Name { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string DocumentType { get; set; }
    
    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; }
    
    
    public ICollection<UserRole> UserRoles { get; } = [];
}