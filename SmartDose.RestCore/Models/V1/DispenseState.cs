namespace SmartDose.RestCore.Models.V1
{
    /// <summary>
    ///     The dispense state.
    /// </summary>
    public enum DispenseState
    {
        /// <summary>
        ///     The dispense init.
        /// </summary>
        DispenseInit,

        /// <summary>
        ///     The dispense started.
        /// </summary>
        DispenseStarted,

        /// <summary>
        ///     The dispense finished.
        /// </summary>
        DispenseFinished,

        /// <summary>
        ///     The dispense canceld.
        /// </summary>
        DispenseCanceled,

        /// <summary>
        ///     The order not yet imported.
        /// </summary>
        OrderNotYetImported,

        /// <summary>
        ///     The order not found.
        /// </summary>
        OrderNotFound
    }
}