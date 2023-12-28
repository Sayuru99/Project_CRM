using FluentValidation.Results;
using MediatR;

namespace MyApp.SharedDomain.Queries
{
    public class QueryBase<TResponse> : IRequest<TResponse>
    {
        public Guid Id { get; set; }

        public virtual bool Valid(out ValidationResult validationResult)
        {
            validationResult = new QueryBaseValidator<TResponse>().Validate(this);
            return validationResult.IsValid;
        }
    }
}
