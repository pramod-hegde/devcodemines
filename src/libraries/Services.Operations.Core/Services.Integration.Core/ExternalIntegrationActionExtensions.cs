using System;
using System.Linq;
using System.Reflection;

namespace Services.Integration.Core
{
    public static class ExternalIntegrationActionExtensions 
    {
        public static string GetExternalIntegrationAction(this Type referenceType) 
        {
            if (referenceType == null)
            {
                return default;
            }

            return referenceType.GetCustomAttribute<ExternalIntegrationActionAttribute>()?.Action;
        }

        public static string GetExternalIntegrationAction(this MethodInfo methodType)
        {
            if (methodType == null)
            {
                return default;
            }

            return methodType.GetCustomAttribute<ExternalIntegrationActionAttribute>()?.Action;
        }

        public static string[] GetExternalIntegrationActions(this Type referenceType)
        {
            if (referenceType == null)
            {
                return default;
            }

            var attributes = referenceType.GetCustomAttributes<ExternalIntegrationActionAttribute>();

            if (attributes == null && !attributes.Any()) 
            {
                return default;
            }

            return attributes.Select(x => x.Action).ToArray();
        }

        public static string[] GetExternalIntegrationActions(this MethodInfo methodType)
        {
            if (methodType == null)
            {
                return default;
            }

            var attributes = methodType.GetCustomAttributes<ExternalIntegrationActionAttribute>();

            if (attributes == null && !attributes.Any())
            {
                return default;
            }

            return attributes.Select(x => x.Action).ToArray();
        }

        public static string GetExternalIntegrationAction(this Type referenceType, string methodWithAttribute)
        {
            if (referenceType == null)
            {
                return default;
            }

            return referenceType.GetMethod(methodWithAttribute)?.GetCustomAttribute<ExternalIntegrationActionAttribute>()?.Action;
        }
    }
}
