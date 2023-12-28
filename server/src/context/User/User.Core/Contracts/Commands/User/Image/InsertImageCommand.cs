using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using MyApp.SharedDomain.Commands;
using User.Core.Contracts.Commands.User.Image.Validators;

namespace User.Core.Contracts.Commands.User.Image
{
    public class InsertImageCommand : InsertCommandBase
    {
        public IFormFile Content { get; set; }

        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new InsertImageCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
