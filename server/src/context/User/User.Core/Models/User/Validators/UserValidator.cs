using FluentValidation;
using MyApp.SharedDomain.Messages;
using User.Core.Models.User.Image.Validators;

namespace User.Core.Models.User.Validators
{
    internal class UserValidator : AbstractValidator<UserModel>
    {
        internal UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty());

            RuleFor(x => x.Email).
                NotEmpty().
                WithMessage(ValidationMessage.NotEmpty());

            RuleFor(x => x.PasswordHash)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty());

            RuleFor(x => x.IsActive)
                .NotEmpty().
                WithMessage(ValidationMessage.NotNull());

            RuleFor(r => r.Image)
                .NotNull()
                .When(x => x.Image is not null)
                .SetValidator(new ImageValidator());
        }
    }
}
