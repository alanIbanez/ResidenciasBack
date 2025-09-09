using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.ValueObjects
{
    public class DNI : ValueObject
    {
        public string Value { get; }

        public DNI(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("DNI cannot be empty");

            if (value.Length < 7 || value.Length > 15)
                throw new DomainException("DNI must be between 7 and 15 characters");

            if (!value.All(char.IsDigit))
                throw new DomainException("DNI must contain only digits");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}