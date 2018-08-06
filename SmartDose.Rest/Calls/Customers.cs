using Flurl;
using Flurl.Http;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SmartDose.Rest.Calls
{
    /*
    public static class Customers
    {
        public static Url Url = Core.Url.AppendPathSegment("Customers");

        public static async Task<HttpStatusCode> CreateCustomerAsync(Models.Customer customer)
        {
            try
            {
                await Url.PostJsonAsync(customer).ConfigureAwait(false);
                return HttpStatusCode.OK;
            }
            catch (FlurlHttpException ex)
            {
                return (ex.Call.Response?.StatusCode ?? HttpStatusCode.RequestTimeout);
            }
        }

        public static async Task<HttpStatusCode> UpdateCustomerAsync(Models.Customer customer)
        {
            try
            {
                await Url.PutJsonAsync(customer).ConfigureAwait(false);
                return HttpStatusCode.OK;
            }
            catch (FlurlHttpException ex)
            {
                return (ex.Call.Response?.StatusCode ?? HttpStatusCode.RequestTimeout);
            }
        }

        public static async Task<HttpStatusCode> DeleteCustomerAsync(Models.Customer customer)
        {
            try
            {
                await Url.AppendPathSegment(customer.CustomerId).DeleteAsync().ConfigureAwait(false);
                return HttpStatusCode.OK;
            }
            catch (FlurlHttpException ex)
            {
                return (ex.Call.Response?.StatusCode ?? HttpStatusCode.RequestTimeout);
            }
        }

        public static async Task<(HttpStatusCode StatusCode, List<Models.Customer> Customers)> GetCustomersAsync()
        {
            try
            {
                var customers = await Url.GetJsonAsync<List<Models.Customer>>().ConfigureAwait(false);
                return (HttpStatusCode.OK, customers);
            }
            catch (FlurlHttpException ex)
            {
                return (ex.Call.Response?.StatusCode ?? HttpStatusCode.RequestTimeout, new List<Models.Customer>());
            }
        }

        public static async Task<HttpStatusCode> DeleteCustomersAsync()
        {
            try
            {
                var response = await GetCustomersAsync().ConfigureAwait(false);
                if (response.StatusCode != HttpStatusCode.OK)
                    return response.StatusCode;
                foreach (var customer in response.Customers)
                    await DeleteCustomerAsync(customer).ConfigureAwait(false);
                return HttpStatusCode.OK;
            }
            catch (FlurlHttpException ex)
            {
                return (ex.Call.Response?.StatusCode ?? HttpStatusCode.RequestTimeout);
            }
        }
    }
    */
}