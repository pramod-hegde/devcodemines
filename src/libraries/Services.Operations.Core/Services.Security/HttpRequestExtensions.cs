using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Services.Security
{
    public static class HttpRequestExtensions
    {
        public static KeyValuePair<string, object>[] ToKeyValuePairCollection(this IHeaderDictionary requestHeaders)
        {
            return requestHeaders?.Where(y => !HttpRequestHeaderLoggingHelper.RequestHeaderLoggingExclusionList.Any(ev => ev.Equals(y.Key?.ToLowerInvariant(), StringComparison.InvariantCultureIgnoreCase)))?.Select(x =>
            {
                string value = string.Empty;
                StringValues values = x.Value;

                if (values != StringValues.Empty && values.Count > 0)
                {
                    if (values.Count == 1)
                    {
                        value = Convert.ToString(values[0]);
                    }
                    else
                    {
                        value = string.Join(", ", values);
                    }

                }

                return new KeyValuePair<string, object>(x.Key, value);
            })?.ToArray();
        }
    }

    class HttpRequestHeaderLoggingHelper
    {
        internal static HashSet<string> RequestHeaderLoggingExclusionList = new HashSet<string>
        {
            "authorization",
            "x-ms-client-principal",
            "x-ms-client-principal-name",
            "x-ms-client-principal-id",
            "x-ms-client-principal-idp",
            "x-arr-ssl",
            "client-ip",
            "x-forwarded-for",
            "x-forwarded-proto",
            "expect",
            "x-appService-proto"
        };
    }
}

