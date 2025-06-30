using FluentValidation;

namespace Application.Features.Auth.Confirm
{
    public class ConfirmValidator : AbstractValidator<ConfirmCommand>
    {
        public ConfirmValidator()
        {
            RuleFor(v => v.UserId)
                .MaximumLength(36)
                .NotEmpty();
        }
    }
}