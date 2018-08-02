using System.Collections.Generic;

namespace SmartDose.RestCore.Models.V1
{
    /// <summary>
    ///     The order result.
    /// </summary>
    public class OrderResult
    {
        public string ExternalID { get; set; }

        public string OrderId { get; set; }

        public string MachineNumber { get; set; }

        public string DispenseState { get; set; }

        public string CreateDate { get; set; }

        public string ProduceDate { get; set; }

        public List<Pouch> Pouches { get; set; }
    }
}