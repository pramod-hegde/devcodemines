namespace Services.Core.Validation.PayloadValidation
{
    [NodeType(ValidationActions.LoopElimination)]
    class LoopElimination : ActionBase
    {
        public LoopElimination(ValidationNode currentNode) : base(currentNode)
        {
        }

        public override IDataValidationResult Validate<T>(T payload, ValidationNode node, ValidationNode currentNodePath, params object[] references)
        {
            _response = new DataValidationResult
            {
                InternalResult = InternalValidationStepResult.LoopElimination,
                Payload = payload
            };
            //_response.ValidationFlow.AddRange(ValidationNode.ValidationFlow);
            return _response;
        }
    }
}
