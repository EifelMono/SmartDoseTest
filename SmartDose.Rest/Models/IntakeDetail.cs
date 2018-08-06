using System.Collections.Generic;

namespace SmartDose.Rest.Models
{
    public class IntakeDetail
    {
        public string IntakeDateTime { get; set; }

        public List<MedicationDetail> MedicationDetails { get; set; }
    }
}