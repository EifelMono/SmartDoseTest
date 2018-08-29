namespace WebServerClient.Models.ExternalOrder
{
    public class MedicationDetail
    {
        /// <summary>
        ///     Gets or sets the medicine id.
        /// </summary>
        public string MedicineId { get; set; }

        /// <summary>
        ///     Gets or sets the count.
        /// </summary>
        public float Count { get; set; }

        /// <summary>
        ///     Gets or sets the intake advice.
        /// </summary>
        public string IntakeAdvice { get; set; }

        /// <summary>
        ///     Gets or sets the physician.
        /// </summary>
        public string Physician { get; set; }

        /// <summary>
        ///     Gets or sets the physician comment.
        /// </summary>
        public string PhysicianComment { get; set; }
    }
}