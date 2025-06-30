using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Auth.SignIn.Commands;

public class SignInExample : IExamplesProvider<SignInCommand>
{
    public SignInCommand GetExamples()
    {
        return new SignInCommand
        {
            UserName = "43735178",
            Password = "User2025$$"
        };
    }
}