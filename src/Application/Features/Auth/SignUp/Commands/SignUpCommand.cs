using Application.Commons.Constants;
using Application.Commons.Mappings;
using Application.Commons.Models;
using AutoMapper;
using Domain.Commons.Enums;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.SignUp.Commands;

public class SignUpCommand : IRequest<Response<SignUpResponse>>, IMapFrom<User>
{
    public string UserName { get; set; }

    public string Password { get; set; }
    
    public string ConfirmPassword { get; set; }

    public string DocumentType { get; set; }

    public string Name { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }
    
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SignUpCommand, User>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(UserStatusEnum), UserStatusEnum.Active)))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => GlobalConstant.DefaultUsername))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));
    }
}