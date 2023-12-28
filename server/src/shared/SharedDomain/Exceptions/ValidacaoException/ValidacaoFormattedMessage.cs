namespace MyApp.SharedDomain.Exceptions.ValidacaoException
{
    public class ValidacaoFormattedMessage
    {
        public string Message { get; }
        public IEnumerable<ValidacaoMessage> Errors { get; }
        public ValidacaoFormattedMessage(string message, IEnumerable<ValidacaoMessage> errors)
        {
            Message = message;
            Errors = errors;
        }
    }
}
