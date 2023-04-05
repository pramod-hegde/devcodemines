namespace Services.Core.Validation.PayloadValidation
{
    public interface IDataValidationResult
    {
        ValidationResults ValidationResult { get; set; }
        PostValidationActions PostValidationAction { get; set; }
        ValidationException ValidationException { get; set; }
        string ValidationMessage { get; set; }
        dynamic Payload { get; set; }
        InternalValidationStepResult InternalResult { get; set; }
        ValidationPath ValidationFlow { get; set; }
    }
}
