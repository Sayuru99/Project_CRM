using FluentValidation.Results;
using MyApp.SharedDomain.Commands;
using User.Core.Contracts.Commands.User.Validators;

namespace User.Core.Contracts.Commands
{
    public class UpdateUserPassword : CommandBase
    {
        public required string ActualPassword { get; set; }
        public required string NewPassword { get; set; }

        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new UpdateUserPasswordCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
