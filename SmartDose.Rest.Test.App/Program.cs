using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl;
using SmartDose.Rest.Extensions;
using System.Diagnostics;

namespace SmartDose.Rest.Test.App
{
    class Program
    {
        static async Task Main(string[] args)
        {


            var SmartDoseUrl = "http://localhost:6040/SmartDose";
            var CustomersUrl = SmartDoseUrl.AppendPathSegment("Customers");
            var CanistersUrl = SmartDoseUrl.AppendPathSegment("Canisters");
            var OrdersUrl = SmartDoseUrl.AppendPathSegment("Orders");

            var x= await OrdersUrl.EmcPostJsonAsync(new Models.ExternalOrder());

            if (await CanistersUrl.EmcGetJsonAsync<List<Models.Canister>>() is var canisters && canisters.IsHttpStatusCodeOK())
            {
                Console.WriteLine($"Canisters={canisters.Self.Count}");
                Console.WriteLine(canisters.Self.Dump());
            }

            Console.ReadLine();
            //if (await CustomersUrl.EmcGetJsonAsync<List<Models.Customer>>() is var customers && customers.IsHttpStatusCodeOK())
            //{

            //    Console.WriteLine(customers.Self.ToJsonString());
            //    Console.WriteLine($"Customers={customers.Self.Count}");
            //    foreach (var customer in customers.Self)
            //        Console.WriteLine(customer);
            //}

            if (await CustomersUrl.AppendPathSegment("4711.1").EmcGetJsonAsync<Models.Customer>() is var customer && customer.IsHttpStatusCodeOK())
            {

                Console.WriteLine(customer.Self.Dump());
            }

            await CustomersUrl.EmcPutJsonAsync(Defaults.Customer("4711.1", "Name 4711.1 Update"));

            Console.ReadLine();
        }
    }
}
