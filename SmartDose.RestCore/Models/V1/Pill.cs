namespace SmartDose.RestCore.Models.V1
{
    /// <summary>
    ///     The pill.
    /// </summary>
    public class Pill
    {
        public string MedicineId { get; set; }

        public string Quantity { get; set; }

        public string Charge { get; set; }
    }
}