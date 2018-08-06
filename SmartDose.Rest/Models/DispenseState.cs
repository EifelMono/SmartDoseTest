namespace SmartDose.Rest.Models
{
    public enum DispenseState
    {
        DispenseInit,
        DispenseStarted,
        DispenseFinished,
        DispenseCanceled,
        OrderNotYetImported,
        OrderNotFound
    }
}