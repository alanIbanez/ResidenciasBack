using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.ValueObjects
{
    public class ExitToken : ValueObject
    {
        public string Value { get; }

        public ExitToken(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Exit token cannot be empty");

            if (value.Length < 10)
                throw new DomainException("Exit token must be at least 10 characters long");

            Value = value;
        }

        public static ExitToken Generate()
        {
            return new ExitToken(Guid.NewGuid().ToString("N")[..20]);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}