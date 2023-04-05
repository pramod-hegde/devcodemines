namespace Services.Core.Validation.PayloadValidation
{ 
    public enum InternalValidationStepResult
    {
        None,
        True,
        False,
        Transform,
        LoopElimination,
        LoopEvaluation,
        Remove,
        ExitAndFalse        
    }
}
