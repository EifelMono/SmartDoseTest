using Flurl;
using FlurlHttp;
using SmartDose.RestCore.Models.V1;

namespace SmartDose.RestCore.REST.V1
{
    public static class Customers
    {
        public static Url Url = Core.Url.AppendPathSegment("Customers");

        public static async (System.Net.HttpStatusCode StatiCode, List<Customer> Customers) Task GetCustomersAsync()
        {
                    var customers = await SmartDoseServer
                                .AppendPathSegment("Customers")
                                .GetJsonAsync<List<Model.Customer>>();
        }
    }
}