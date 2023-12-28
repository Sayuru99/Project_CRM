using FluentValidation.Results;
using MyApp.SharedDomain.Commands;

namespace User.Core.Contracts.Commands
{
    public class UpdateUserCommand : CommandBase
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } // TODO: Transformar em uma propriedade do entity padrão.
        public string? Password { get; set; }
        public DateTime? CreatedAt { get; set; } // TODO: Remover daqui futuramente, melhorar abstração de created e updated no command.

        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new UpdateUserCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
