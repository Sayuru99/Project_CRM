namespace MyApp.SharedDomain.ValueObjects
{
    public sealed class Cnpj : ValueObject
    {
        private readonly string value;

        public Cnpj(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("CNPJ value cannot be null or empty.", nameof(value));
            }

            value = value.Replace(".", "").Replace("-", "").Replace("/", "");

            if (!IsValid(value))
            {
                throw new ArgumentException("Invalid CNPJ.", nameof(value));
            }

            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return value.Equals(((Cnpj)obj).value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override object GetValue()
        {
            return value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return value;
        }

        private static bool IsValid(string value)
        {
            if (value.Length != 14 || !long.TryParse(value, out _))
            {
                return false;
            }

            return IsChecksumValid(value);
        }

        private static bool IsChecksumValid(string value)
        {
            int[] multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var digits = value.ToCharArray();
            int sum = 0;

            for (int i = 0; i < 12; i++)
            {
                sum += (digits[i] - '0') * multiplier1[i];
            }

            int mod = sum % 11;
            int digit1 = (mod < 2) ? 0 : 11 - mod;

            if ((digits[12] - '0') != digit1)
            {
                return false;
            }

            sum = 0;

            for (int i = 0; i < 13; i++)
            {
                sum += (digits[i] - '0') * multiplier2[i];
            }

            mod = sum % 11;
            int digit2 = (mod < 2) ? 0 : 11 - mod;

            return (digits[13] - '0') == digit2;
        }
    }
}