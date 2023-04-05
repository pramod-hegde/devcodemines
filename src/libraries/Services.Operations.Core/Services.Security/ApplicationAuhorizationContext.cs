using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Services.Security
{
    public class ApplicationAuhorizationContext
    {
        public ApplicationAuhorizationContext()
        {

        }

        public ApplicationAuhorizationContext(IConfiguration configuration)
        {
            AppId = Convert.ToString(configuration.GetSection("AppId").Value);
            Description = Convert.ToString(configuration.GetSection("Description").Value);
            AuthDomain = Convert.ToString(configuration.GetSection("AuthDomain").Value);
            AuthContext = new AuthContextCollection();
            PopuateAuthCollection(configuration.GetSection("AuthContext").GetChildren());
        }

        private void PopuateAuthCollection(IEnumerable<IConfiguration> items)
        {
            foreach (var item in items)
            {
                AuthContext.Add(Convert.ToString(item.GetSection("Context").Value), Convert.ToString(item.GetSection("Value").Value));
            }
        }

        public string AppId { get; set; }
        public string Description { get; set; }
        public string AuthDomain { get; set; }
        public AuthContextCollection AuthContext { get; set; }

        public static implicit operator string(ApplicationAuhorizationContext value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static implicit operator ApplicationAuhorizationContext(string value)
        {
            JObject bm = JsonConvert.DeserializeObject<JObject>(value);

            var c = new ApplicationAuhorizationContext
            {
                AppId = Convert.ToString(bm["AppId"]),
                Description = Convert.ToString(bm["Description"]),
                AuthDomain = Convert.ToString(bm["AuthDomain"]),
                AuthContext = new AuthContextCollection()
            };

            var authContext = Convert.ToString(bm["AuthContext"]);
            if (string.IsNullOrWhiteSpace(authContext))
            {
                return c;
            }


            var ac = JsonConvert.DeserializeObject<List<JObject>>(authContext);
            foreach (var a in ac)
            {
                AuthMetadata v = a.ToString();
                c.AuthContext.Add(v.Context, v.Value);
            }

            return c;
        }
    }
}
