namespace WebServerClient.Models.Manufacturer
{
    internal class Manufacturer
    {
        /// <summary>
        ///     Visible identifier for the manufacturer
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        ///     Gets or sets the contact address.
        /// </summary>
        public virtual ContactAddress Address { get; set; }

        /// <summary>
        ///     Gets or sets the comment about the manufacturer.
        /// </summary>
        public virtual string Comment { get; set; }

        /// <summary>
        ///     Gets or sets the name of the manufacturer.
        /// </summary>
        public virtual string Name { get; set; }
    }
}