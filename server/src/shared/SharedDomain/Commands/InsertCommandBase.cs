using FluentValidation.Results;
using MediatR;

namespace MyApp.SharedDomain.Commands
{
    public abstract class InsertCommandBase : IRequest<CommandResponse>
    {
        public abstract bool Valid(out ValidationResult validationResult);
    }
}
