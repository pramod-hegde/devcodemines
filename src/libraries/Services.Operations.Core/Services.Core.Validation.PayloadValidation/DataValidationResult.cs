namespace Services.Core.Validation.PayloadValidation
{
    class DataValidationResult : IDataValidationResult
    {
        internal DataValidationResult()
        {
            ValidationFlow = new ValidationPath();
        }

        public ValidationResults ValidationResult { get; set; }
        public PostValidationActions PostValidationAction { get; set; }
        public ValidationException ValidationException { get; set; }
        public string ValidationMessage { get; set; }
        public object Payload { get; set; }
        public InternalValidationStepResult InternalResult { get; set; }
        public ValidationPath ValidationFlow { get; set; }
    }
}
