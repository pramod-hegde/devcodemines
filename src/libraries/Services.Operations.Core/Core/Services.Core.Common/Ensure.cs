using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Core.Common
{
    public static class Ensure
    {
        public static void NotNull<T> (string argumentName, T value)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);
        }

        public static void NotEmpty (string argumentName, string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException(Resources.NonEmptyStringExpected, argumentName);
        }


        public static void NotNullOrEmpty<T> (string argumentName, IEnumerable<T> value)
        {
            if (value == null || !value.Any())
                throw new ArgumentNullException(argumentName);
        }
    }
}
