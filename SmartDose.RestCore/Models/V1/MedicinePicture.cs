using System;

namespace SmartDose.RestCore.Models.V1
{
    [Serializable]
    public class MedicinePicture
    {
        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the picture data.
        /// </summary>
        public string Picture { get; set; }
    }
}