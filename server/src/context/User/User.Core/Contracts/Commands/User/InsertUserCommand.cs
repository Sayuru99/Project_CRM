using FluentValidation.Results;
using MyApp.SharedDomain.Commands;
using User.Core.Contracts.Commands.User.Image;
using User.Core.Contracts.Commands.User.Validators;

namespace User.Core.Contracts.Commands
{
    public class InsertUserCommand : InsertCommandBase
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public InsertImageCommand? Image { get; set; }

        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new InsertUserCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
