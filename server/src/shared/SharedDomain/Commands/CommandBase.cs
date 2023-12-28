using FluentValidation.Results;
using MediatR;

namespace MyApp.SharedDomain.Commands
{
    public abstract class CommandBase : IRequest<CommandResponse>
    {
        public required Guid Id { get; set; }

        public DateTime UpdatedAt { get; } = DateTime.UtcNow;

        public abstract bool Valid(out ValidationResult validationResult);
    }
}
