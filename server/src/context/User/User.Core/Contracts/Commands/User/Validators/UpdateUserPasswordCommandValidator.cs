using FluentValidation;
using MyApp.SharedDomain.Messages;
using User.Core.Contracts.Commands.User.Image;

namespace User.Core.Contracts.Commands.User.Validators
{
    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPassword>
    {
        public UpdateUserPasswordCommandValidator()
        {
            RuleFor(r => r.ActualPassword)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());

            RuleFor(r => r.NewPassword)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());
        }
    }
}
