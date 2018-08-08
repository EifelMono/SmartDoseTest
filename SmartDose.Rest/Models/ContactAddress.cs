using Newtonsoft.Json;

namespace SmartDose.Rest.Models
{
    public class ContactAddress
    {
        [JsonProperty("Addressline1")]
        public string AddressLine1 { get; set; }

  
        public string City { get; set; }

        public string Country { get; set; }

        public string NameLine1 { get; set; }

        [RestValidation(MaxStringLength = 25)]
        public string Postalcode { get; set; }
       
        public string State { get; set; }
    }
}