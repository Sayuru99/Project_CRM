using FluentValidation.Results;
using MyApp.SharedDomain.Commands;
using User.Core.Contracts.Commands.User.Validators;

namespace User.Core.Contracts.Commands
{
    public class InactiveUserCommand : CommandBase
    {
        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new InactiveUserCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
