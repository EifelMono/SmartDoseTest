using System.Collections.Generic;

namespace SmartDose.RestCore.Models.V1
{
    public class OrderDetail
    {
        /// <summary>
        ///     Gets or sets the pharmacy.
        /// </summary>
        public Customer Pharmacy { get; set; }

        /// <summary>
        ///     Gets or sets the patient.
        /// </summary>
        public Patient Patient { get; set; }

        /// <summary>
        ///     Gets or sets the destination facility.
        /// </summary>
        public DestinationFacility DestinationFacility { get; set; }

        /// <summary>
        ///     Gets or sets the external detail print info 1.
        /// </summary>
        public string ExternalDetailPrintInfo1 { get; set; }

        /// <summary>
        ///     Gets or sets the external detail print info 2.
        /// </summary>
        public string ExternalDetailPrintInfo2 { get; set; }

        /// <summary>
        ///     Gets or sets the intake details.
        /// </summary>
        public List<IntakeDetail> IntakeDetails { get; set; }
    }
}