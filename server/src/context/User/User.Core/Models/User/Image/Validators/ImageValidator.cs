using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace User.Core.Models.User.Image.Validators
{
    internal class ImageValidator : AbstractValidator<ImageModel>
    {
        internal ImageValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty());
        }
    }
}
