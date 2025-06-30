using Application.Commons.Mappings;
using Domain.Entities;

namespace Application.Features.Auth.Confirm;

public class ConfirmResponse : IMapFrom<User>
{
    public Guid Id { get; set; }
    
    public string UserName { get; set; } 

    public string Email { get; set; }

    public string Name { get; set; }
}