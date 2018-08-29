﻿namespace WebServerClient.Models.Customer
{
    public class Customer
    {
        /// <summary>
        ///     Gets or sets the customer id.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the website.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        ///     Gets or sets the fax.
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        ///     Gets or sets the contact address.
        /// </summary>
        public ContactAddress ContactAddress { get; set; }

        /// <summary>
        ///     Gets or sets the contact person.
        /// </summary>
        public ContactPerson ContactPerson { get; set; }
    }
}