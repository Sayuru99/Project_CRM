using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace User.Core.Contracts.Commands.User.Validators
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());
        }
    }
}
