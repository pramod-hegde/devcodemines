using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Services.Security
{
    public class AuthMetadata
    {
        public string Context { get; set; }
        public object Value { get; set; }
        public static implicit operator string(AuthMetadata value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static implicit operator AuthMetadata(string value)
        {
            JObject bm = JsonConvert.DeserializeObject<JObject>(value);
            return new AuthMetadata
            {
                Context = Convert.ToString(bm["Context"]),
                Value = Convert.ChangeType(Convert.ToString(bm["Value"]), TypeCode.Object),
            };
        }
    }
}
