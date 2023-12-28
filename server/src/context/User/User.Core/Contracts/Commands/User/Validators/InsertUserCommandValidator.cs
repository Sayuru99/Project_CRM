using FluentValidation;
using MyApp.SharedDomain.Messages;
using User.Core.Contracts.Commands.User.Image.Validators;

namespace User.Core.Contracts.Commands.User.Validators
{
    public class InsertUserCommandValidator : AbstractValidator<InsertUserCommand>
    {
        public InsertUserCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());

            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required())
                .EmailAddress()
                .WithMessage("Invalid email.");

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());

            RuleFor(r => r.Image)
                .NotNull()
                .When(x => x.Image is not null)
                .SetValidator(new InsertImageCommandValidator());
        }
    }
}
