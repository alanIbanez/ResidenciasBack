using Domain.Exceptions;
using Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Value { get; }

        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Phone number cannot be empty");

            if (!IsValidPhoneNumber(value))
                throw new DomainException("Invalid phone number format");

            Value = NormalizePhoneNumber(value);
        }

        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Remove all non-digit characters
            var digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());
            return digitsOnly.Length >= 10 && digitsOnly.Length <= 15;
        }

        private static string NormalizePhoneNumber(string phoneNumber)
        {
            // Remove all non-digit characters
            var digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());
            return digitsOnly;
        }

        public string ToFormattedString()
        {
            if (Value.Length == 10)
            {
                return $"({Value[..3]}) {Value[3..6]}-{Value[6..]}";
            }
            return Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}