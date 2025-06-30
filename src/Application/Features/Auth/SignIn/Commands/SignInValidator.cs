using FluentValidation;

namespace Application.Features.Auth.SignIn.Commands;

public class SignInValidator : AbstractValidator<SignInCommand>
{
    public SignInValidator()
    {
        RuleFor(v => v.UserName)
            .MaximumLength(12)
            .NotEmpty();
            
        RuleFor(v => v.Password)
            .MaximumLength(200)
            .NotEmpty();
    }
}