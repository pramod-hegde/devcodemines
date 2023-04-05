using System;

namespace Services.Core.Common
{
    public static class TypesHelper
    {
        public static bool IsOpenGenericType (Type type, object openGenericType)
        {
            Ensure.NotNull("type", type);

            if (type.IsGenericType)
                type = type.GetGenericTypeDefinition();

            return type.Equals(openGenericType);
        }
    }
}
