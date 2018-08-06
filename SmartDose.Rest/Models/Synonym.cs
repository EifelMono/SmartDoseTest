using System;

namespace SmartDose.Rest.Models
{
    public class Synonym
    {
        public string Identifier { get; set; }
        public decimal Price { get; set; }

        public int Content { get; set; }

        public string Description { get; set; }
    }
}