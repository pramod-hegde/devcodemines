namespace Services.Core.Validation.PayloadValidation
{
    [NodeType(ValidationActions.Remove)]
    class RemoveItem : ActionBase
    {
        public RemoveItem(ValidationNode currentNode) : base(currentNode)
        {
        }

        public override IDataValidationResult Validate<T>(T payload, ValidationNode node, ValidationNode currentNodePath, params object[] references)
        {
            _response = new DataValidationResult
            {
                InternalResult = InternalValidationStepResult.Remove,
                Payload = payload
            };
            //_response.ValidationFlow.AddRange(ValidationNode.ValidationFlow);
            return _response;
        }
    }
}
