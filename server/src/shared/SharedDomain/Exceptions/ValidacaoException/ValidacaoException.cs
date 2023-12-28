using System.Net;
using FluentValidation.Results;
using MyApp.SharedDomain.Exceptions;

namespace MyApp.SharedDomain.Exceptions.ValidacaoException
{
    public class ValidacaoException : ExceptionBase
    {
        private readonly List<ValidacaoMessage> _erros;
        public ValidacaoFormattedMessage FormatedMessage => new(Message, _erros);

        public ValidacaoException(string message, ValidationResult validationResult) : base(message, HttpStatusCode.BadRequest)
        {
            _erros = new List<ValidacaoMessage>();

            foreach (var error in validationResult.Errors)
            {
                _erros.Add(new ValidacaoMessage(error));
            }
        }
    }
}
