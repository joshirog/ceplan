using Application.Commons.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Auth.Me.Queries;

public class GetMeResponse : IMapFrom<User>
{
    public string Id { get; set; }
    
    public string DocumentType { get; set; }

    public string UserName { get; set; }

    public string Name { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string Email { get; set; }

    public string PhoneNumber { get; set; }
    public GetMeRoleResponse Role { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Role, GetMeRoleResponse>();
        
        profile.CreateMap<User, GetMeResponse>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}

public class GetMeRoleResponse : IMapFrom<Role>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}