using System;

namespace Domain.Exceptions
{
    public class DomainException : Exception
    {
        public string ErrorCode { get; }
        public int StatusCode { get; }

        public DomainException(string message) : base(message)
        {
            ErrorCode = "DOMAIN_ERROR";
            StatusCode = 400;
        }

        public DomainException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = 400;
        }

        public DomainException(string message, string errorCode, int statusCode) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = "DOMAIN_ERROR";
            StatusCode = 400;
        }

        public DomainException(string message, string errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
            StatusCode = 400;
        }

        public DomainException(string message, string errorCode, int statusCode, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }
}