using System.Collections.Generic;

namespace SmartDose.RestCore.Models.V1
{
    public class ExternalOrder
    {
        /// <summary>
        ///     Gets or sets the external id.
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        ///     Gets or sets the time-stamp.
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        ///     Gets or sets the customer.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        ///     Gets or sets the order details.
        /// </summary>
        public List<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        public OrderState State { get; set; }
    }
}