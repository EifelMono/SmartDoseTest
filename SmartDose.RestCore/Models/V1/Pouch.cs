using SmartDose.RestCore.Models.V1;
using System.Collections.Generic;

namespace SmartDose.RestCore.Models.V1
{
    /// <summary>
    ///     The pouch.
    /// </summary>
    public class Pouch
    {
        public string PouchId { get; set; }

        public string PouchType { get; set; }

        public string PatienId { get; set; }

        public List<Pill> Pills { get; set; }

        //ToDo Added attribute Spindle from OrderResult
        public string Spindle { get; set; }
    }
}