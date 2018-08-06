namespace SmartDose.Rest.Models
{
    public enum OrderState
    {
        Undefined,
        ValidationOk,
        ValidationFailed,
        ReadyForProduction,
        InQueue,
        ProductionFinished,
        ProductionCancelled,
        InProduction,
        ReValidationRequested,
        NewRepairOrder
    }
}