using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Services.Security
{
    public static partial class AssemblyBindingRedirectHelper
    {
        ///<summary>
        /// Reads the "BindingRedirecs" field from the app settings and applies the redirection on the
        /// specified assemblies
        /// </summary>
        public static void ConfigureBindingRedirects()
        {
            var redirects = GetBindingRedirects();

            if (redirects != null && redirects.Any())
            {
                redirects.ForEach(RedirectAssembly);
            }
        }

        private static List<BindingRedirect> GetBindingRedirects()
        {
            try
            {
                var bindingRedirectListJson = Environment.GetEnvironmentVariable("BindingRedirects");

                if (string.IsNullOrWhiteSpace(bindingRedirectListJson))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<List<BindingRedirect>>(bindingRedirectListJson);
            }
            catch
            {
                return null;
            }
        }

        private static void RedirectAssembly(BindingRedirect bindingRedirect)
        {
            ResolveEventHandler handler = null;
            handler = (sender, args) =>
            {
                var requestedAssembly = new AssemblyName(args.Name);
                if (requestedAssembly.Name != bindingRedirect.ShortName)
                {
                    return null;
                }
                var targetPublicKeyToken = new AssemblyName("x, PublicKeyToken=" + bindingRedirect.PublicKeyToken).GetPublicKeyToken();
                requestedAssembly.SetPublicKeyToken(targetPublicKeyToken);
                requestedAssembly.Version = new Version(bindingRedirect.RedirectToVersion);
                requestedAssembly.CultureInfo = CultureInfo.InvariantCulture;
                AppDomain.CurrentDomain.AssemblyResolve -= handler;
                return Assembly.Load(requestedAssembly);
            };
            AppDomain.CurrentDomain.AssemblyResolve += handler;
        }
    }
}
