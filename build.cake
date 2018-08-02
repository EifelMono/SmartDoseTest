#region Setup
#addin nuget:?package=Flurl&version=2.8.0
#addin nuget:?package=Flurl.Http&version=2.3.2
#addin nuget:?package=Newtonsoft.Json&version=11.0.2
#addin nuget:?package=Cake.Incubator&version=2.0.2

using Newtonsoft.Json;
using Flurl.Http;
using Cake.Incubator;

#r ".\SmartDose.RestCore\bin\Debug\net45\SmartDose.RestCore.dll"
using SmartDose.RestCore.Helpers;
using SmartDose.RestCore.Models;
using Model = SmartDose.RestCore.Models.V1;
#endregion


#region Customers
Task("GetCustomers")
.Does(()=> {
    System.Threading.Tasks.Task.Run(async ()=> {
        var customers = await "http://localhost:6040/smartdose/Customers/".GetJsonAsync<List<Model.Customer>>();
        Information($"Customers={customers.Count}");
        Information(customers.Dump());
    }).Wait();
});

Task("CreateCustomer")
.Does(()=> {
    System.Threading.Tasks.Task.Run(async ()=> {
        var customer = new Model.Customer
        {
            CustomerId = "4711",
            Name = "Name4711",
            Description= "Hallo",
        };
        var response = await "http://localhost:6040/SmartDose/Customers".PostJsonAsync(customer);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            Information($"Customer created {customer.CustomerId}");
        else
            Error(response);
        
        var customers = await "http://localhost:6040/smartdose/Customers/".GetJsonAsync<List<Model.Customer>>();
        Information($"Customers={customers.Count}");
    }).Wait();
});

Task("UpdateCustomer")
.Does(()=> {
    System.Threading.Tasks.Task.Run(async ()=> {
        var customer = new Model.Customer
        {
            CustomerId = "4711",
            Name = "Name4711-"+ DateTime.Now.ToString(),
            Description= "Hallo",
        };
        var response = await $"http://localhost:6040/SmartDose/Customers/{customer.CustomerId}".PutJsonAsync(customer);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            Information($"Customer created {customer.CustomerId}");
        else
            Error(response);
        
        var customers = await "http://localhost:6040/smartdose/Customers/".GetJsonAsync<List<Model.Customer>>();
        Information($"Customers={customers.Count}");
    }).Wait();
});
#endregion


#region Canister
Task("GetCanisters")
.Does(()=> {
    System.Threading.Tasks.Task.Run(async ()=> {
        var canisters = await "http://localhost:6040/smartdose/Canisters/".GetJsonAsync<List<Model.Canister>>();
        Information($"Canisters={canisters.Count}");
        Information(canisters.Dump());
    }).Wait();
});

Task("CreateCanisters")
.Does(()=> {
    System.Threading.Tasks.Task.Run(async ()=> {
        var canister = new Model.Canister
        {
            CanisterId= "Canister4711",
            Rfid = 1.ToRfId(),
            Largecanister = false,
            RotorId= 1.ToRotorId(),
        };
        var response = await "http://localhost:6040/SmartDose/Canisters".AllowHttpStatus("400-500").PostJsonAsync(canister);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            Information($"Canister created {canister.CanisterId}");
        else
            Error($"StatusCode={(int)response.StatusCode}={response.StatusCode}");
        
        var canisters = await "http://localhost:6040/smartdose/Canisters/".GetJsonAsync<List<Model.Canister>>();
        Information($"Canisters={canisters.Count}");
    }).Wait();
})
.OnError(exception =>
{
    Error($"Creating Canister {exception}");
});
#endregion

#region Cake defaults
var target = Argument("target", "Default");
Task("Default")
.Does(() => {
   Information("Hello Cake!");
});

RunTarget(target);
#endregion
