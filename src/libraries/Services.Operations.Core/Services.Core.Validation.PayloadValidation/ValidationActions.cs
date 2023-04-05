namespace Services.Core.Validation.PayloadValidation
{
    enum ValidationActions
    {
        Rule,
        Sequence,
        Conditional,
        Transform,
        Save,
        SaveAndProcess,
        LoopValidation,
        LoopElimination,
        Remove,
        Exit,
        ExitAndFalse
    }
}
