namespace Services.Core.Validation.PayloadValidation
{
    [NodeType(ValidationActions.Exit)]
    class Exit : ActionBase
    {
        public Exit(ValidationNode currentNode) : base(currentNode)
        {
        }

        public override IDataValidationResult Validate<T>(T payload, ValidationNode node, ValidationNode currentNodePath, params object[] references)
        {
            _response = new DataValidationResult
            {
                ValidationResult = ValidationResults.ValidationCompleted,
                PostValidationAction = PostValidationActions.NoAction,
                Payload = payload
            };
            //_response.ValidationFlow.AddRange(ValidationNode.ValidationFlow);
            return _response;
        }
    }
}
