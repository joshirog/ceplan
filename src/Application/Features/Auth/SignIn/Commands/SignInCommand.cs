using Application.Commons.Models;
using MediatR;

namespace Application.Features.Auth.SignIn.Commands;

public class SignInCommand : IRequest<Response<SignInResponse>>
{
    public string UserName { get; set; }

    public string Password { get; set; }
}