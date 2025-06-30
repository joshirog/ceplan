using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Auth.SignUp.Commands;

public class SignUpExample : IExamplesProvider<SignUpCommand>
{
    public SignUpCommand GetExamples()
    {
        return new SignUpCommand
        {
            DocumentType = "DNI",
            UserName = "43735178",
            Email = "joshirog@gmail.com",
            Name = "Jose Luis",
            FirstName = "Oshiro",
            LastName = "Gushiken",
            PhoneNumber = "946678198",
            Password = "User2025$$",
            ConfirmPassword = "User2025$$"
        };
    }
}