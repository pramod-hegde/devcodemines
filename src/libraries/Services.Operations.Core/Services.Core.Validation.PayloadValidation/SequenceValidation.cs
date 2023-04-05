using System.Linq;
using System.Linq.Dynamic.Core;

namespace Services.Core.Validation.PayloadValidation
{
    [NodeType(ValidationActions.Sequence)]
    class SequenceValidation : ActionBase
    {
        public SequenceValidation(ValidationNode currentNode) : base(currentNode)
        {
        }

        public override IDataValidationResult Validate<T>(T payload, ValidationNode node, ValidationNode currentNodePath, params object[] references)
        {           
            return __currentNode.Sequences.First(x => x.Id == "1").Validate(payload, currentNodePath, references);
        }
    }
}
