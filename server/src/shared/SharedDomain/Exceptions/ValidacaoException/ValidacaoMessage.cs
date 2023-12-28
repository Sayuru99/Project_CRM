using FluentValidation.Results;

namespace MyApp.SharedDomain.Exceptions.ValidacaoException
{
    public class ValidacaoMessage
    {
        public string PropertyName { get; }
        public string ErrorMessage { get; }

        public ValidacaoMessage(ValidationFailure failure)
        {
            PropertyName = failure.PropertyName;
            ErrorMessage = failure.ErrorMessage;
        }
    }
}
