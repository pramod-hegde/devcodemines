using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Services.Core.Validation.PayloadValidation
{
    public class ValidationNodeCollection : Collection<ValidationNode>
    {
        public ValidationNodeCollection()
        {
        }

        public ValidationNodeCollection(IList<ValidationNode> list) : base(list)
        {
        }

        public ValidationNode this[string key]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    return null;
                }

                return this.FirstOrDefault(x => x.EventType == key);
            }
        }
    }
}
