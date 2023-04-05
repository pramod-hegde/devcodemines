namespace Services.Core.Validation.PayloadValidation
{
    public interface IConnector
    {
        ValidationNode Next(IDataValidationResult result, ValidationNode node, ValidationNode config, object[] references);
    }
}
