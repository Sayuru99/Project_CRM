using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace User.Core.Contracts.Commands.User.Validators
{
    public class InactiveUserCommandValidator : AbstractValidator<InactiveUserCommand>
    {
        public InactiveUserCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());
        }
    }
}
