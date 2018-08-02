namespace SmartDose.RestCore.Models.V1
{
    public class Canister
    {
        /// <summary>
        ///     Gets or sets the canister id.
        /// </summary>
        public string CanisterId { get; set; }

        /// <summary>
        ///     Gets or sets the RFID.
        /// </summary>
        public string Rfid { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether large canister.
        /// </summary>
        public bool Largecanister { get; set; }

        /// <summary>
        ///     Gets or sets the rotor id.
        /// </summary>
        public string RotorId { get; set; }
    }
}