using Application.Commons.Constants;
using Application.Commons.Exceptions;
using Application.Commons.Interfaces;
using Application.Commons.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.SignIn.Commands;

public class SignInHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IMediator mediator) : IRequestHandler<SignInCommand, Response<SignInResponse>>
{
    public async Task<Response<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.UserName);
        
        var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);
            
        if (result.IsNotAllowed)
            throw new ErrorInvalidException([ResponseConstant.Confirm]);

        if (user?.LockoutEnd is not null)
        {
            await mediator.Publish(new SignInNotification{ User = user }, cancellationToken);
            throw new ErrorInvalidException([ResponseConstant.LockedAccount]);
        }
        
        if(!result.Succeeded)
            throw new ErrorInvalidException([ResponseConstant.SignInFail]);
        
        var expiration = DateTime.Now.AddSeconds(ConfigurationConstant.GetJwtExpiration);

        return Response.Ok(ResponseConstant.Success, new SignInResponse
        {
            Type = JwtBearerDefaults.AuthenticationScheme,
            AccessToken = await jwtService.CreateAccessToken(user, expiration),
            Expiration = expiration
        });
    }
}