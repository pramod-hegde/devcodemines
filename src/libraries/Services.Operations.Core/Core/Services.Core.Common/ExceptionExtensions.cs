using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Core.Common
{
    public static class ExceptionExtensions
    {
        public static IEnumerable<string> Messages (this Exception ex)
        {
            // return an empty sequence if the provided exception is null
            if (ex == null) { yield break; }

            // first return THIS exception's message at the beginning of the list
            yield return ex.Message;

            // then get all the lower-level exception messages recursively (if any)
            IEnumerable<Exception> innerExceptions = Enumerable.Empty<Exception>();

            if (ex is AggregateException && (ex as AggregateException).InnerExceptions != null && (ex as AggregateException).InnerExceptions.Any())
            {
                innerExceptions = (ex as AggregateException).InnerExceptions;
            }
            else if (ex.InnerException != null)
            {
                innerExceptions = new Exception[] { ex.InnerException };
            }

            foreach (var innerEx in innerExceptions)
            {
                foreach (string msg in innerEx.Messages())
                {
                    yield return msg;
                }
            }
        }
    }
}
