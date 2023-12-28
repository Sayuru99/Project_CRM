namespace MyApp.SharedDomain.Messages
{
    public class ValidationMessage
    {
        private const string MAXIMUM_LENGTH = "The maximum length for the field is {0}.";
        private const string NOT_EMPTY = "The field cannot be empty.";
        private const string NOT_NULL = "The field cannot be null.";
        private const string REQUIRED = "Field is required.";
        private const string CANNOT_FILLED = "The field cannot be filled.";

        public static string MaxiMumLenght(int length)
        {
            return string.Format(MAXIMUM_LENGTH, length);
        }

        public static string NotEmpty(string condition)
        {
            return Message(NOT_EMPTY, condition);
        }

        public static string NotEmpty()
        {
            return NotEmpty(string.Empty);
        }

        public static string NotNull(string condition)
        {
            return Message(NOT_NULL, condition);
        }

        public static string NotNull()
        {
            return NotNull(string.Empty);
        }

        public static string Required(string condition) 
        { 
            return Message(REQUIRED, condition); 
        }

        public static string Required()
        {
            return Required(string.Empty);
        }

        public static string CannotFilled(string condition)
        {
            return Message(CANNOT_FILLED, condition);
        }

        public static string CannotFilled()
        {
            return CannotFilled(string.Empty);
        }

        private static string Message(string message, string condition)
        {
            return $"{message} {condition}".Trim();
        }
    }
}
