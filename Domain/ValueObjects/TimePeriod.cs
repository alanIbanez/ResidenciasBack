using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.ValueObjects
{
    public class TimePeriod : ValueObject
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public TimePeriod(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                throw new DomainException("Start date must be before end date");

            if ((endDate - startDate).TotalDays > 365)
                throw new DomainException("Time period cannot exceed 1 year");

            StartDate = startDate;
            EndDate = endDate;
        }

        public bool Overlaps(TimePeriod other)
        {
            return StartDate < other.EndDate && other.StartDate < EndDate;
        }

        public bool Contains(DateTime date)
        {
            return date >= StartDate && date <= EndDate;
        }

        public TimeSpan Duration => EndDate - StartDate;

        public int Days => (int)Duration.TotalDays;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartDate;
            yield return EndDate;
        }

        public override string ToString() => $"{StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd}";
    }
}