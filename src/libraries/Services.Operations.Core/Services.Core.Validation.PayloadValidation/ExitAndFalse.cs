namespace Services.Core.Validation.PayloadValidation
{
    [NodeType(ValidationActions.ExitAndFalse)]
    class ExitAndFalse : ActionBase
    {
        public ExitAndFalse(ValidationNode currentNode) : base(currentNode)
        {
        }

        public override IDataValidationResult Validate<T>(T payload, ValidationNode node, ValidationNode currentNodePath, params object[] references)
        {
            _response = new DataValidationResult
            {
                InternalResult = InternalValidationStepResult.ExitAndFalse,
                Payload = payload,
                ValidationResult = ValidationResults.ValidationCompleted,
                PostValidationAction = PostValidationActions.NoAction
            };
            //_response.ValidationFlow.AddRange(ValidationNode.ValidationFlow);
            return _response;
        }
    }
}
