
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
    => StatusMessage(response.StatusCode, message);

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
            if (await MedicinesUrl.EmcGetJsonAsync<List<Models.Medicine>>() is var medicines && medicines.IsHttpStatusCodeOK())
            {
                Information($"Medicines={medicines.Self.Count}");
                Information(medicines.Self.Dump());
            }
            StatusMessage(medicines.StatusCode, "GetMedicines");
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
async Task CreateMedicineFromExternalOrderAsync(Models.ExternalOrder externalOrder)
{
    foreach(var orderDetail in externalOrder.OrderDetails)
        foreach(var intakeDetail in orderDetail.IntakeDetails)
            foreach(var medicationDetail in intakeDetail.MedicationDetails)
                await CreateMedicineAsync(Defaults.Medicine(medicationDetail.MedicineId, medicationDetail.PrescribedMedicine)).ConfigureAwait(false);
}
async Task CreateExternalOrderAsync(string jsonFilename)
{
    var externalOrder= jsonFilename.FileReadJson<Models.ExternalOrder>();
    Information($"Order {externalOrder.ExternalId}");
    await CreateMedicineFromExternalOrderAsync(externalOrder).ConfigureAwait(false);
    StatusMessage(await OrdersUrl.EmcPostJsonAsync(externalOrder).ConfigureAwait(false), $"Create External order");
}


#endregion
#region Tickets 
#region Ticket 1804

Task("Ticket-Sw-1804-Test")
    .Does(async ()=> {
        await CreateExternalOrderAsync("./Tickets/SW-1804/Test.json");
    });
/* 
Task("Ticket-Sw-1804-Working")
    .Does(()=> {
        System.Threading.Tasks.Task.Run(async ()=> {
            await CreateExternalOrder("./Tickets/SW-1804/20180724-ROWATest49-JSON-working.json").ConfigureAwait(false);
    }).Wait();
});
Task("Ticket-Sw-1804-LongText-not-Working")
    .Does(()=> {
        System.Threading.Tasks.Task.Run(async ()=> {
            await CreateExternalOrder("./Tickets/SW-1804/20180724-ROWATest51-JSON-Longtext-not_working.json").ConfigureAwait(false);
    }).Wait();
});
*/
#endregion
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
