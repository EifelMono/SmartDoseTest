using System;
using System.Collections.Generic;

namespace SmartDose.RestCore.Models.V1
{
    [Serializable]
    public class Medicine
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Medicine" /> class.
        /// </summary>
        public Medicine()
        {
            SynonymIds = new List<Synonym>();
            PrintDetails = new List<PrintDetail>();
            Pictures = new List<MedicinePicture>();
        }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether active.
        /// </summary>
        public bool Active { get; set; }


        //Note:TrayFillOnly is not defined in rest interface but can be used 
        /// <summary>
        ///     Gets or sets a value indicating whether tray fill only.
        /// </summary>
        public virtual bool TrayFillOnly { get; set; }

        //Note: PouchMode is not defined in rest interface but can be used
        /// <summary>
        ///     Gets or sets the pouch mode.
        /// </summary>
        public virtual PouchMode PouchMode { get; set; }

        /// <summary>
        ///     Gets or sets the synonym ids.
        /// </summary>
        public List<Synonym> SynonymIds { get; set; }

        /// <summary>
        ///     Gets or sets the print details.
        /// </summary>
        public List<PrintDetail> PrintDetails { get; set; }

        /// <summary>
        ///     Gets or sets the pictures.
        /// </summary>
        public List<MedicinePicture> Pictures { get; set; }

        /// <summary>
        ///     Gets or sets the special handling.
        /// </summary>
        public SpecialHandling SpecialHandling { get; set; }
    }
}