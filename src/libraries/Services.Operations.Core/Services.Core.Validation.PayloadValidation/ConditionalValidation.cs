using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Services.Core.Validation.PayloadValidation
{
    [NodeType(ValidationActions.Conditional)]
    class ConditionalValidation : ActionBase
    {
        public ConditionalValidation(ValidationNode currentNode) : base(currentNode)
        {
        }

        public override IDataValidationResult Validate<T>(T payload, ValidationNode node, ValidationNode currentNodePath, params object[] references)
        {
            return ConditionalEvaluator(payload, node.Expression, node.ReferenceObject, references);
        }

        IDataValidationResult ConditionalEvaluator<T>(T payload, string expression, string referObject, object[] reference = default)
        {
            var response = new DataValidationResult();
            try
            {
                var inputObject = Expression.Parameter(payload.GetType(), payload.GetType().Name);
                if (!string.IsNullOrEmpty(referObject))
                {
                    var referenceObject = reference.First(x => x.GetType().Name == referObject);
                    var refObject = Expression.Parameter(referenceObject.GetType(), referenceObject.GetType().Name);
                    var result = Convert.ToBoolean(DynamicExpressionParser.ParseLambda(new[] { inputObject, refObject }, null, expression).Compile().DynamicInvoke(payload, referenceObject));
                    response = new DataValidationResult
                    {
                        InternalResult = result ? InternalValidationStepResult.True : InternalValidationStepResult.False,
                        Payload = payload
                    };
                    return response;
                }
                else
                {
                    var result = Convert.ToBoolean(DynamicExpressionParser.ParseLambda(new[] { inputObject }, null, expression).Compile().DynamicInvoke(payload));
                    response = new DataValidationResult
                    {
                        InternalResult = result ? InternalValidationStepResult.True : InternalValidationStepResult.False,
                        Payload = payload
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                response = new DataValidationResult
                {
                    ValidationResult = ValidationResults.DataValidationFailure,
                    ValidationException = new ValidationException(ex.Message, ex),
                    Payload = payload
                };
                response.ValidationFlow.AddRange(ValidationNode.ValidationFlow);
                throw new ValidationException("Conditional Evaluation Failed", new object[] { expression, payload, response });
            }

        }
    }
}
