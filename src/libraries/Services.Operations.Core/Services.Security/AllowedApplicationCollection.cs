using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Services.Security
{
    public class AllowedApplicationCollection : Collection<ApplicationAuhorizationContext>
    {
        public AllowedApplicationCollection()
        {
        }

        public AllowedApplicationCollection(IList<ApplicationAuhorizationContext> list) : base(list)
        {
        }
    }
}
