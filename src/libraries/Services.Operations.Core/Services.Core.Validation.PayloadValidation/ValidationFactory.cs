namespace Services.Core.Validation.PayloadValidation
{
    public delegate ValidationNode ValidationContextHandler(string eventType);

    public class ValidationFactory : IValidationFactory
    {
        readonly ValidationContextHandler _validationHandle;
        readonly string _eventType;

        public ValidationFactory(ValidationContextHandler validationHandle, string eventType)
        {
            _validationHandle = validationHandle;
            _eventType = eventType;
        }

        IDataValidationResult IValidationFactory.ValidatePayload<T>(T payload, params object[] references)
        {
            ValidationNode validationContext = _validationHandle(_eventType);

            if (validationContext != null)
            {
                var response = validationContext.Validate(payload, validationContext, references);
                response.ValidationFlow.AddRange(ValidationNode.ValidationFlow);
                ValidationNode.ValidationFlow.Clear();
                return response;
            }
            else
            {               
                var response = new DataValidationResult
                {
                    ValidationResult = ValidationResults.DataValidationFailure,
                    PostValidationAction = PostValidationActions.NoAction,
                    ValidationException = new ValidationException("Rules associated with the event called not found")
                };
                throw new ValidationException("Rules associated with the event called not found", new object[] { _eventType });
            }
        }
    }
}
