﻿namespace SmartDose.RestCore.Models.V1
{
    public enum OrderState
    {
        /// <summary>
        ///     "Unknown": No information about job state (failing connection etc.)
        /// </summary>
        Undefined,

        /// <summary>
        ///     "Passed Verification": After automatic verification for received external jobs through the interface the job gets
        ///     depending on the result of the verification one of this states.
        /// </summary>
        ValidationOk,

        /// <summary>
        ///     "Failed Verification": After automatic verification for received external jobs through the interface the job gets
        ///     depending on the result of the verification one of this states.
        /// </summary>
        ValidationFailed,

        /// <summary>
        ///     Status of an internal order where the ProductionSystem plug-in is looking for.
        /// </summary>
        ReadyForProduction,

        /// <summary>
        ///     Status of an internal order which is finish converted into a list of MachineOrders. Sent to a device. Is on device
        ///     queue.
        /// </summary>
        InQueue,

        /// <summary>
        ///     After job is completely produced.
        /// </summary>
        ProductionFinished,

        /// <summary>
        ///     A job send to the machine was canceled on the sdmc-device.
        /// </summary>
        ProductionCancelled,

        /// <summary>
        ///     Currently processed by a device.
        /// </summary>
        InProduction,

        /// <summary>
        ///     Revalidation of the order was requested
        /// </summary>
        ReValidationRequested,

        /// <summary>
        ///     Newly created sub-Order from an existing InternalOrder
        /// </summary>
        NewRepairOrder
    }
}