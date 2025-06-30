using Application.Commons.Constants;
using Application.Commons.Exceptions;
using Application.Commons.Models;
using AutoMapper;
using Domain.Commons.Enums;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.SignUp.Commands;

public class SignUpHandler(IMapper mapper, UserManager<User> userManager, IMediator mediator) : IRequestHandler<SignUpCommand, Response<SignUpResponse>>
{
    public async Task<Response<SignUpResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        
        var identity = await userManager.CreateAsync(user, request.Password);
        
        if (!identity.Succeeded)
            throw new ErrorInvalidException(identity.Errors.Select(x => x.Description));

        identity = await userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleEnum), RoleEnum.Client)!);

        if (!identity.Succeeded)
            throw new ErrorInvalidException(identity.Errors.Select(x => x.Description));
        
        await mediator.Publish(new SignUpNotification{ User = user }, cancellationToken);
        
        return identity.Succeeded ? 
            Response.Ok(ResponseConstant.Success, mapper.Map<SignUpResponse>(user)) : 
            Response.Fail(ResponseConstant.Error, new SignUpResponse());
    }
}