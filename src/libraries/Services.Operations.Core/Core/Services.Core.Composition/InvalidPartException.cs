using System;
using System.Runtime.Serialization;

namespace Services.Core.Composition
{
    class InvalidPartException : Exception
    {
        public InvalidPartException ()
        {
        }

        public InvalidPartException (string message) : base(message)
        {
        }

        public InvalidPartException (string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPartException (SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
