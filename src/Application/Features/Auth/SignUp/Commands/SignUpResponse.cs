using Application.Commons.Mappings;
using Domain.Entities;

namespace Application.Features.Auth.SignUp.Commands;

public class SignUpResponse : IMapFrom<User>
{
    public Guid Id { get; set; }
    
    public string UserName { get; set; } 

    public string Email { get; set; } 
}