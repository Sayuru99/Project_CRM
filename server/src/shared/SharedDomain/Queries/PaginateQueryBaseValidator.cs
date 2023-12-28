using FluentValidation;

namespace MyApp.SharedDomain.Queries
{
    public class PaginateQueryBaseValidator<TResponse> : AbstractValidator<PaginateQueryBase<TResponse>>
    {
        public PaginateQueryBaseValidator()
        {
            RuleFor(r => r.Page)
                .NotEmpty()
                .WithMessage("The field Page cannot be empty.");

            RuleFor(r => r.PageSize)
                .NotEmpty()
                .WithMessage("The field PageSize cannot be empty.");
        }
    }
}
