using System;

namespace WebServerClient.Models.Medicine
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