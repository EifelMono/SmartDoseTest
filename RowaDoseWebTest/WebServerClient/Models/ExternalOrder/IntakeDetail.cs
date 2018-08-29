using System.Collections.Generic;

namespace WebServerClient.Models.ExternalOrder
{
    public class IntakeDetail
    {
        /// <summary>
        ///     Gets or sets the intake date time.
        /// </summary>
        public string IntakeDateTime { get; set; }

        /// <summary>
        ///     Gets or sets the medication details.
        /// </summary>
        public List<MedicationDetail> MedicationDetails { get; set; }
    }
}