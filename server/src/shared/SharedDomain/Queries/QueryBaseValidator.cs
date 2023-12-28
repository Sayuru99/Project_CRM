using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace MyApp.SharedDomain.Queries
{
    public class QueryBaseValidator<TResponse> : AbstractValidator<QueryBase<TResponse>>
    {
        public QueryBaseValidator()
        {
            // TODO: Thinking about how to implement this validation, improving the way to conceptualize the parameter.
            //RuleFor(r => r.Param)
            //    .NotEmpty()
            //    .WithMessage(ValidationMessage.NotEmpty());
        }
    }
}
