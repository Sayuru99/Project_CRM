using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace MyApp.SharedDomain.Commands
{
    public abstract class CommandBaseValidador : AbstractValidator<CommandBase>
    {
        public CommandBaseValidador()
        {
            RuleFor(r => r.Id)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty());
        }
    }
}
