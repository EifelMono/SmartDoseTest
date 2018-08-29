﻿namespace WebServerClient.Models.Order
{
    public class OrderStatus
    {
        public string ExternalID { get; set; }

        public string OrderId { get; set; }

        public string CreateDate { get; set; }

        public string DispenseState { get; set; }

        public string MachineNumber { get; set; }
    }
}