namespace Services.Core.Validation.PayloadValidation
{
    [NodeType(ValidationActions.LoopValidation)]
    class LoopValidation : ActionBase
    {
        public LoopValidation(ValidationNode currentNode) : base(currentNode)
        {
        }

        public override IDataValidationResult Validate<T>(T payload, ValidationNode node, ValidationNode currentNodePath, params object[] references)
        {
            _response = new DataValidationResult
            {
                InternalResult = InternalValidationStepResult.LoopEvaluation,
                Payload = payload
            };
            //_response.ValidationFlow.AddRange(ValidationNode.ValidationFlow);
            return _response;
        }
    }
}
