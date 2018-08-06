using Newtonsoft.Json;

namespace SmartDose.Rest.Models
{
    public class ContactAddress
    {
        [JsonProperty("Addressline1")]
        public string AddressLine1;

        public string City;

        public string Country;

        public string NameLine1;

        public string Postalcode;

        public string State;
    }
}