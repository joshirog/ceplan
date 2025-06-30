using FluentValidation;

namespace Application.Features.Auth.SignUp.Commands;

public class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(x => x.DocumentType)
            .NotEmpty()
            .MaximumLength(3);
        
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(15);
        
        RuleFor(v => v.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(100)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{8,}$").WithMessage("The password is insecure, please try entering a new one. It must contain at least one lowercase letter, one uppercase letter, numbers, and at least one special character.");
            
        RuleFor(v => v.Password)
            .Equal(v => v.ConfirmPassword).WithMessage("The password entered must be the same as the confirmed password.");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(200)
            .EmailAddress();
        
        RuleFor(v => v.Name)
            .MaximumLength(100)
            .NotEmpty();
        
        RuleFor(v => v.FirstName)
            .MaximumLength(100)
            .NotEmpty();
            
        RuleFor(v => v.LastName)
            .MaximumLength(100)
            .NotEmpty();
        
        RuleFor(v => v.PhoneNumber)
            .MaximumLength(9)
            .NotEmpty();
    }
}