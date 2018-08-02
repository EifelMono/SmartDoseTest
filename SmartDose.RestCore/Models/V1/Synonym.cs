using System;

namespace SmartDose.RestCore.Models.V1
{
    public class Synonym
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        ///     Gets or sets the price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Gets or sets the content.
        /// </summary>
        public int Content { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}