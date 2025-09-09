using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class TimeRange : ValueObject
    {
        public TimeSpan Start { get; }
        public TimeSpan End { get; }

        public TimeRange(TimeSpan start, TimeSpan end)
        {
            if (start >= end)
                throw new DomainException("Start time must be before end time");

            Start = start;
            End = end;
        }

        public bool Overlaps(TimeRange other)
        {
            return Start < other.End && other.Start < End;
        }

        public bool Contains(TimeSpan time)
        {
            return time >= Start && time <= End;
        }

        public TimeSpan Duration => End - Start;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }

    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }
    }
}