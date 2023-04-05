namespace Services.Core.Validation.PayloadValidation
{
    interface IValidator
    {
        IDataValidationResult Validate<T>(T payload, ValidationNode node, ValidationNode currentNodePath, params object[] references);
    }
}
