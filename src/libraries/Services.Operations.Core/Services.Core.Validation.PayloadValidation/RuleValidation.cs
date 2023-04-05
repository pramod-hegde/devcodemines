using System.Linq;
using System.Linq.Dynamic.Core;

namespace Services.Core.Validation.PayloadValidation
{
    [NodeType(ValidationActions.Rule)]
    class RuleValidation : ActionBase
    {
        public RuleValidation(ValidationNode currentNode) : base(currentNode)
        {
        }

        public override IDataValidationResult Validate<T>(T payload, ValidationNode node, ValidationNode currentNodePath, params object[] references)
        {
            return __currentNode.Sequences?.First(x => x.Id == "S1").Validate(payload, currentNodePath, references);
        }
    }
}
