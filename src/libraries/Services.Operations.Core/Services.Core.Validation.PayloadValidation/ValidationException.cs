using System;
using System.Runtime.Serialization;

namespace Services.Core.Validation.PayloadValidation
{
    public class ValidationException : Exception
    {
        public ValidationException()
        {
        }
        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, object[] values) : base(message)
        {
            Values = values;
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public object[] Values { get; set; }

    }
}
