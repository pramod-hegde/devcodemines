namespace Services.Core.Validation.PayloadValidation
{ 
    public interface IValidationFactory
    {
        IDataValidationResult ValidatePayload<T>(T payload, params object[] references);
    }
}
