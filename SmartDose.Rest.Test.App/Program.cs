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

            {
                var order = "20180724-ROWATest49-JSON-working.json".FileReadJson<Models.ExternalOrder>();
                var stopwatch = Stopwatch.StartNew();
                var iMax = 1;
                for (int i = 0; i < iMax - 1; i++)
                {
                    var result1 = RestValidation.Check(order, "Orders");
                }

                var result = RestValidation.Check(order, "Order");
                Console.WriteLine($"result={result.Ok}");
                Console.WriteLine($"infos\r\n{result.Infos}");
                stopwatch.Stop();
                Console.WriteLine($"run={iMax} all time={stopwatch.ElapsedMilliseconds} time={(stopwatch.ElapsedMilliseconds * 1.0) / iMax}");

                Console.ReadLine();

            
            }


            {
                var contact = new Models.ContactAddress
                {
                    NameLine1 = "123",
                    Postalcode = "1234567890123456789012345678901234567890",
                };

                var result = RestValidation.Check(contact, "Contact");

                Console.WriteLine($"result={result.Ok}");
                Console.WriteLine($"infos\r\n{result.Infos}");

                Console.ReadLine();
            }

            var SmartDoseUrl = "http://localhost:6040/SmartDose";
            var CustomersUrl = SmartDoseUrl.AppendPathSegment("Customers");
            var CanistersUrl = SmartDoseUrl.AppendPathSegment("Canisters");



            await CanistersUrl.EmcPostJsonAsync(Defaults.Canister(10));

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
