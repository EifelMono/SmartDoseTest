
#region Overhead to run the test 
#region Setup
#addin nuget:?package=Newtonsoft.Json&version=11.0.2
#addin nuget:?package=Flurl&version=2.7.1
#addin nuget:?package=Flurl.Http&version=2.3.2
using Newtonsoft.Json;
using Flurl;
using Flurl.Http;

#r ".\SmartDose.Rest\bin\Debug\netstandard2.0\SmartDose.Rest.dll"
using SmartDose.Rest;
using SmartDose.Rest.Models;
using SmartDose.Rest.Extensions;
using Models = SmartDose.Rest.Models;
#endregion

#region Helper
var SmartDoseServer = new Url("http://localhost:6040/smartdose/");
                        // .AllowHttpStatus("400-500");
var SmartDoseUrl= "http://localhost:6040/SmartDose"; 
var CustomersUrl= "http://localhost:6040/SmartDose/Customers";
var CanistersUrl= "http://localhost:6040/SmartDose/Canisters";

var MedicinesUrl= "http://localhost:6040/SmartDose/Medicines";

var OrdersUrl= "http://localhost:6040/SmartDose/Orders";


void StatusMessage(System.Net.HttpStatusCode statusCode, string message= "")
{
    var statusCodeAsString= statusCode.ToString();
    if (statusCode < 0)
        statusCodeAsString= ((EmcHttpStatusCode)statusCode).ToString();
    var viewMessage= $"{message} StatusCode={statusCodeAsString} [{(int)statusCode}]".Trim(); 
    if (statusCode == System.Net.HttpStatusCode.OK)
        Information(viewMessage);
    else
        Error(viewMessage);
}

void StatusMessage(EmcFlurHttpResponse response, string message= "")
    => StatusMessage(response.StatusCode, string.IsNullOrEmpty(message) ? response.ResponseMessage: $"{message}\r\n{response.ResponseMessage}");

#endregion

#region Customers
Task("GetCustomers")
    .Does(async ()=> {
            if (await CustomersUrl.EmcGetJsonAsync<List<Models.Customer>>() is var customers && customers.IsHttpStatusCodeOK())
            {
                Information($"Customers={customers.Self.Count}");
                Information(customers.Self.Dump());
            }
            StatusMessage(customers.StatusCode, "GetCustomers");
});

Task("GetCustomer")
    .Does(async ()=> {
            if (await CustomersUrl
                .AppendPathSegments("4711.1")
                .EmcGetJsonAsync<Models.Customer>() is var customer && customer.IsHttpStatusCodeOK())
                Information(customer.Self.Dump());
            StatusMessage(customer.StatusCode, "GetCustomer");
});

Task("CreateCustomer")
    .Does(async ()=> {
        StatusMessage(await CustomersUrl.EmcPostJsonAsync(Defaults.Customer("4711.1")), "Create Customer");
});

Task("UpdateCustomer")
    .Does(async ()=> {
        StatusMessage(await CustomersUrl
                        .AppendPathSegment("4711.1")
                        .EmcPutJsonAsync(Defaults.Customer("4711.1", "Name 4711.1 Update")), "Update Customer");
});

Task("DeleteCustomer")
    .Does(async ()=> {
        StatusMessage(await CustomersUrl
                        .AppendPathSegment("4711.1")
                        .EmcDeleteAsync(), "Delete Customer");
});

Task("DeleteCustomers")
    .Does(async ()=> {
            if (await CustomersUrl.EmcGetJsonAsync<List<Models.Customer>>() is var customers && customers.IsHttpStatusCodeOK())
            {
                Information($"Customers={customers.Self.Count}");
                foreach(var customer in customers.Self)
                    StatusMessage(await CustomersUrl.AppendPathSegment(customer.CustomerId).EmcDeleteAsync(), $"Delete customer name={customer.Name} id={customer.CustomerId}");
            }
            StatusMessage(customers.StatusCode, "DeleteCustomers");
});

#endregion

#region Canisters

Task("GetCanisters")
    .Does(async ()=> {
            if (await CanistersUrl.EmcGetJsonAsync<List<Models.Canister>>() is var canisters && canisters.IsHttpStatusCodeOK())
            {
                Information($"Canisters={canisters.Self.Count}");
                Information(canisters.Self.Dump());
            }
            StatusMessage(canisters.StatusCode, "GetCanisters");
});

Task("CreateCanister")
    .Does(async ()=> {
        for(int i=1;i<= 10; i++)
        StatusMessage(await CanistersUrl.EmcPostJsonAsync(Defaults.Canister(i)), $"Create Canister {i}");
});


Task("DeleteCanisters")
    .Does(async ()=> {
            if (await CanistersUrl.EmcGetJsonAsync<List<Models.Canister>>() is var canisters && canisters.IsHttpStatusCodeOK())
            {
                Information($"Canisters={canisters.Self.Count}");
                foreach(var canister in canisters.Self)
                    StatusMessage(await CanistersUrl.AppendPathSegment(canister.CanisterId).EmcDeleteAsync(), $"Delete canister id={canister.CanisterId}");
            }
            StatusMessage(canisters.StatusCode, "DeleteCanisters");
});

#endregion

#region Medicines

Task("GetMedicines")
    .Does(async ()=> {
            var time= System.Diagnostics.Stopwatch.StartNew();
            if (await MedicinesUrl.EmcGetJsonAsync<List<Models.Medicine>>() is var medicines && medicines.IsHttpStatusCodeOK())
            {
                time.Stop();
                Information($"Medicines={medicines.Self.Count}");
                // Information(medicines.Self.Dump());
            }
            StatusMessage(medicines.StatusCode, "GetMedicines");
            Information($"Elapsed time={time.Elapsed}");
});

Task("CreateMedicines")
    .Does(async ()=> {
        for(int i=1;i<= 10; i++)
        StatusMessage(await MedicinesUrl.EmcPostJsonAsync(Defaults.Medicine(i.ToString())), $"Create Medicine {i}");
});

Task("DeleteMedicines")
    .Does(async ()=> {
            if (await MedicinesUrl.EmcGetJsonAsync<List<Models.Medicine>>() is var medicines && medicines.IsHttpStatusCodeOK())
            {
                Information($"Medicines={medicines.Self.Count}");
                foreach(var medicine in medicines.Self)
                    StatusMessage(await MedicinesUrl.AppendPathSegment(medicine.MedicineId).EmcDeleteAsync(), $"Delete medicines id={medicine.MedicineId}");
            }
            StatusMessage(medicines.StatusCode, "DeleteMedicines");
});

Task("PostMedicines_11")
    .Does(async ()=> {
        StatusMessage(await MedicinesUrl.EmcPostJsonAsync(Defaults.Medicine("11")), $"Create Medicine 11");
});

Task("PutMedicines_11")
    .Does(async ()=> {
        StatusMessage(await MedicinesUrl.EmcPostJsonAsync(Defaults.Medicine("11")), $"Create Medicine 11");
});

Task("DeleteMedicines_11")
    .Does(async ()=> {
        StatusMessage(await MedicinesUrl.AppendPathSegment("11").EmcDeleteAsync(), $"Delete medicines id=11");
});
#endregion

#region ExternalOrder


Task("GetOrders")
    .Does(async ()=> {
            if (await OrdersUrl.EmcGetJsonAsync<List<Models.OrderDetail>>() is var orders && orders.IsHttpStatusCodeOK())
            {
                Information($"Orders={orders.Self.Count}");
                Information(orders.Self.Dump());
            }
            StatusMessage(orders.StatusCode, "GetOrders");
});

/* 
Task("DeleteAllOrders")
    .Does(()=> {
        System.Threading.Tasks.Task.Run(async ()=> {
            var orders = await SmartDoseServer
                                    .AppendPathSegment("Orders")
                                    .GetJsonAsync<List<Model.OrderDetail>>();
            foreach(var order in orders)
            {
                var response = await SmartDoseServer
                                        .AppendPathSegments("Orders", order.Identifier)
                                        .DeleteAsync();
                ResponseMessage(response.StatusCode, "Orders delete");
            }
        }).Wait();
    });
*/
#endregion

#region Ticket Helper
async Task CreateMedicineAsync(Models.Medicine medicine)
{
    StatusMessage(await MedicinesUrl.EmcPostJsonAsync(medicine).ConfigureAwait(false), $"Create Medicine {medicine.Name}");
}

async Task CreateCanisterAsync(Models.Canister canister)
{
    StatusMessage(await CanistersUrl.EmcPostJsonAsync(canister).ConfigureAwait(false), $"Create Canister {canister.CanisterId}");
}
async Task CreateMedicineFromExternalOrderAsync(Models.ExternalOrder externalOrder)
{
    foreach(var orderDetail in externalOrder.OrderDetails)
        foreach(var intakeDetail in orderDetail.IntakeDetails)
            foreach(var medicationDetail in intakeDetail.MedicationDetails)
                await CreateMedicineAsync(Defaults.Medicine(medicationDetail.MedicineId, medicationDetail.PrescribedMedicine)).ConfigureAwait(false);
}
/* 
async Task CreateCanisterFromExternalOrderAsync(Models.ExternalOrder externalOrder)
{
    foreach(var orderDetail in externalOrder.OrderDetails)
        foreach(var intakeDetail in orderDetail.IntakeDetails)
            foreach(var medicationDetail in intakeDetail.MedicationDetails)
                await CreateCansiterAsync(Defaults.Canister(medicationDetail.MedicineId, medicationDetail.PrescribedMedicine)).ConfigureAwait(false);
}
*/

async Task CreateExternalOrderAsync(string jsonFilename, bool withMedicine= true)
{
    var externalOrder= jsonFilename.FileReadJson<Models.ExternalOrder>();
    Information($"Order {externalOrder.ExternalId}");
    if (withMedicine)
    await CreateMedicineFromExternalOrderAsync(externalOrder).ConfigureAwait(false);
    StatusMessage(await OrdersUrl.EmcPostJsonAsync(externalOrder).ConfigureAwait(false), $"Create External order");
}




#endregion

#endregion

#region Tickets 
#region Ticket 1804

Task("Ticket-Sw-1804-test.json")
    .Does(async ()=> {
            await CreateExternalOrderAsync("./Tickets/SW-1804/test.json", false);
    });
Task("Ticket-Sw-1804-test1MedicinNotExist.json")
    .Does(async ()=> {
            await CreateExternalOrderAsync("./Tickets/SW-1804/test1MedicinNotExist.json", false);
    });    

Task("Ticket-Sw-1804-OrderGenerator")
    .Does(async ()=> {
            await CreateExternalOrderAsync("./Tickets/SW-1804/OrderGenerator.json", true);
    });    

Task("Ticket-Sw-1804-Working")
    .Does(async ()=> {
        await CreateExternalOrderAsync("./Tickets/SW-1804/20180724-ROWATest49-JSON-working.json");
    });

Task("Ticket-Sw-1804-LongText-not-Working")
    .Does(async ()=> {
        await CreateExternalOrderAsync("./Tickets/SW-1804/20180724-ROWATest51-JSON-Longtext-not_working.json");
    });

#endregion

#region Ticket 1805

Task("Ticket-Sw-1805-Test")
    .Does(async ()=> {
        try
        {
            var s= System.Diagnostics.Stopwatch.StartNew();
            await CreateExternalOrderAsync("./Tickets/SW-1805/test.json", false);
            Information($"time={s.Elapsed}");
        }
        catch(Exception ex)
        {
            Error(ex.ToString());
        }
    });
    
Task("Ticket-Sw-1805")
    .Does(async ()=> {
        try {
         await CreateExternalOrderAsync("./Tickets/SW-1805/20180726_132546-JSON-TEST54-Bug-Medifilm-IntakeAdvice-Unicode.json");
        }
        catch(Exception ex)
        {
            Error(ex.ToString());
        }
    });

#endregion


#region GetMedicin

Task("Ticket-GetMedicines-tictacwhite")
    .Does(async ()=> {
        try {
            var time= System.Diagnostics.Stopwatch.StartNew();
            if (await "http://localhost:6040/SmartDose/Medicines/tictacwhite".EmcGetJsonAsync<Models.Medicine>() is var medicines && medicines.IsHttpStatusCodeOK())
            {
                time.Stop();
                Information(medicines.Self.Dump());
            }
            StatusMessage(medicines.StatusCode, "GetMedicines");
            Information($"Elapsed time={time.Elapsed}");
        }
        catch(Exception ex)
        {
            Error(ex.ToString());
        }
});
Task("Ticket-GetMedicines-All")
    .Does(async ()=> {
            var time= System.Diagnostics.Stopwatch.StartNew();
            if (await MedicinesUrl.EmcGetJsonAsync<List<Models.Medicine>>() is var medicines && medicines.IsHttpStatusCodeOK())
            {
                time.Stop();
                Information($"Medicines={medicines.Self.Count}");
                // Information(medicines.Self.Dump());
            }
            StatusMessage(medicines.StatusCode, "GetMedicines");
            Information($"Elapsed time={time.Elapsed}");
});

Task("Ticket-GetMedicines-16258721000171111")
    .Does(async ()=> {
        try {
            var time= System.Diagnostics.Stopwatch.StartNew();
            if (await "http://localhost:6040/SmartDose/Medicines/16258721000171111".EmcGetJsonAsync<Models.Medicine>() is var medicines && medicines.IsHttpStatusCodeOK())
            {
                time.Stop();
                Information(medicines.Self.Dump());
            }
            StatusMessage(medicines.StatusCode, "GetMedicines");
            Information($"Elapsed time={time.Elapsed}");
        }
        catch(Exception ex)
        {
            Error(ex.ToString());
        }
});

Task("Ticket-GetMedicines")
    .IsDependentOn("Ticket-GetMedicines-tictacwhite")
    .IsDependentOn("Ticket-GetMedicines-All")
    .IsDependentOn("Ticket-GetMedicines-16258721000171111")
    .Does(()=> {
});
#endregion
#endregion


#region Test

async Task TestExternalOrderAsync(string jsonFilename, int count)
{
    var externalOrder= jsonFilename.FileReadJson<Models.ExternalOrder>();
    var intakeDetails = externalOrder.OrderDetails.First().IntakeDetails;
            intakeDetails.Clear();
            for (int i = 0; i < count; i++)
            {
                intakeDetails.Add(new IntakeDetail
                {
                    IntakeDateTime = DateTime.Now.AddDays(i).ToString(),
                    MedicationDetails = new List<MedicationDetail>
                    {
                        new MedicationDetail
                        {
                            MedicineId= i.ToString(),
                            Count= 1,
                            PrescribedMedicine= $"PrescribedMedicine {i}",
                            IntakeAdvice= $"IntakeAdvice {i}",
                            PhysicianComment= $"PhysicianComment {i}",
                            Physician= $"Physician {i}",
                        }
                    }
                });
            }
    var ordersUrl= "http://localhost:6040/SmartDose/Orders?CheckMedicine=true"; 
    StatusMessage(await ordersUrl.EmcPostJsonAsync(externalOrder).ConfigureAwait(false), $"Create External order");
}

Task("Ticket-Test.test.json")
    .Does(async ()=> {
            var times= new List<(int Count, long Time)>();
            foreach(var count in new int [] {
                1,
                2,
                5,
                10,
                50, 
                100, 
                1000, 
                100,
                50, 
                10,
                5,
                2,
                1})
            {
                var stopwatch= System.Diagnostics.Stopwatch.StartNew();
                await TestExternalOrderAsync("./Tickets/Test/test.json", count);
                stopwatch.Stop();
                times.Add((count, stopwatch.ElapsedMilliseconds));
                Information($"Count={count} time={stopwatch.ElapsedMilliseconds} ms time per element={stopwatch.ElapsedMilliseconds/count} ");
            }
            foreach(var time in times)
                Information($"Count={time.Count,5}\ttime={time.Time, 10} ms\ttime per element={time.Time/time.Count, 10} ");
    });


#endregion
#region Cake defaults
var target = Argument("target", "Default");

Task("Default")
    .Does(() => {
        Information("Build dependence project for this cake!");
        NuGetRestore("./SmartDose.RestCore/SmartDose.RestCore.csproj");
        DotNetCoreBuild("./SmartDose.RestCore/SmartDose.RestCore.csproj");
});
RunTarget(target);
#endregion
