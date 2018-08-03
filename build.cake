#region Setup
#addin nuget:?package=Flurl&version=2.7.1
#addin nuget:?package=Flurl.Http&version=2.3.2
#addin nuget:?package=Newtonsoft.Json&version=11.0.2
#addin nuget:?package=Cake.Incubator&version=2.0.2

using Newtonsoft.Json;
using Flurl;
using Flurl.Http;
using Cake.Incubator;

#r ".\SmartDose.RestCore\bin\Debug\netstandard2.0\SmartDose.RestCore.dll"
using SmartDose.RestCore.Helpers;
using SmartDose.RestCore.Models;
using Model = SmartDose.RestCore.Models.V1;
#endregion

#region Helper
var SmartDoseServer ="http://localhost:6040/smartdose/";
                        // .AllowHttpStatus("400-500");
var SmartDoseServerWithHttpStatus = SmartDoseServer.AllowHttpStatus("400-500");

void ResponseMessage(System.Net.HttpStatusCode statusCode, string messsage= "")
{
    if (statusCode == System.Net.HttpStatusCode.OK)
        Information($"{messsage}");
    else
        Error($"{messsage} StatusCode={(int)statusCode}={statusCode}");

}
#endregion

#region Customers
Task("GetCustomers")
.Does(()=> {
    System.Threading.Tasks.Task.Run(async ()=> {
        var customers = await SmartDoseServer
                                .AppendPathSegment("Customers")
                                .GetJsonAsync<List<Model.Customer>>();
        Information($"Customers={customers.Count}");
        Information(customers.Dump());
    }).Wait();
});

Task("DeleteAllCustomers")
    .Does(()=> {
        System.Threading.Tasks.Task.Run(async ()=> {
            var customers = await SmartDoseServer
                                    .AppendPathSegment("Customers")
                                    .GetJsonAsync<List<Model.Customer>>();
            foreach(var customer in customers)
            {
                var response = await SmartDoseServer
                                        .AppendPathSegments("Customers", customer.CustomerId)
                                        .DeleteAsync();
                ResponseMessage(response.StatusCode, "Customer delete");
            }
        }).Wait();
    });

Task("CreateCustomer")
    .Does(()=> {
        System.Threading.Tasks.Task.Run(async ()=> {
            try {
                var customer = new Model.Customer
                {
                    CustomerId = "4711",
                    Name = "Name4711",
                    Description= "Hallo",
                };
                var response = await SmartDoseServerWithHttpStatus
                                        .AppendPathSegment("Customers")
                                        .PostJsonAsync(customer);
                ResponseMessage(response.StatusCode, $"Customer create {customer.Name}");
                
                var customers = await SmartDoseServer
                                        .AppendPathSegment("Customers")
                                        .GetJsonAsync<List<Model.Customer>>();
                Information($"Customers={customers.Count}");
            }
            catch(Exception ex)
            {
                Error(ex.ToString());
            }
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
            var response = await SmartDoseServer
                                    .AppendPathSegments("Customers", customer.CustomerId)
                                    .PutJsonAsync(customer);
            ResponseMessage(response.StatusCode, $"Customer update {customer.Name}");
            
            var customers = await SmartDoseServer
                                    .AppendPathSegment("Customers")
                                    .GetJsonAsync<List<Model.Customer>>();
            Information($"Customers={customers.Count}");
        }).Wait();
    });
#endregion

#region Canister
Task("GetCanisters")
    .Does(()=> {
        System.Threading.Tasks.Task.Run(async ()=> {
            var canisters = await SmartDoseServer
                                    .AppendPathSegment("Canisters")
                                    .GetJsonAsync<List<Model.Canister>>();
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
            var response = await SmartDoseServerWithHttpStatus
                                    .AppendPathSegment("Canisters")
                                    .PostJsonAsync(canister);
             ResponseMessage(response.StatusCode, $"Canister created {canister.CanisterId}");
            
            var canisters = await SmartDoseServer
                                    .AppendPathSegment("Canisters")
                                    .GetJsonAsync<List<Model.Canister>>();
            Information($"Canisters={canisters.Count}");
    }).Wait();
})
.OnError(exception =>
{
    Error($"Creating Canister {exception}");
});
#endregion

#region Medicine
Task("GetMedicines")
    .Does(()=> {
        System.Threading.Tasks.Task.Run(async ()=> {
            var medicines = await SmartDoseServer
                                .AppendPathSegment("Medicines")
                                .GetJsonAsync<List<Model.Medicine>>();
            Information($"Medicines={medicines.Count}");
            Information(medicines.Dump());
        }).Wait();
    });

Task("DeleteAllMedicines")
    .Does(()=> {
        System.Threading.Tasks.Task.Run(async ()=> {
            var medicines = await SmartDoseServer
                                    .AppendPathSegment("Medicines")
                                    .GetJsonAsync<List<Model.Medicine>>();
            foreach(var medicine in medicines)
            {
                var response = await SmartDoseServer
                                        .AppendPathSegments("Medicines", medicine.Identifier)
                                        .DeleteAsync();
                ResponseMessage(response.StatusCode, "Medicines delete");
            }
        }).Wait();
    });

Task("CreateMedicine")
    .Does(()=> {
        System.Threading.Tasks.Task.Run(async ()=> {
            var medicine= new Model.Medicine{
                                Active = true,
                                Comment = "Comment " + Guid.NewGuid().ToString(),
                                Description = "Med Desc " + Guid.NewGuid().ToString(),
                                Identifier = "3",
                                Name = "Medicine 3",
                                Pictures = new List<Model.MedicinePicture>(),
                                PouchMode = Model.PouchMode.MultiDose,
                                PrintDetails = new List<Model.PrintDetail>(),
                                SpecialHandling = new Model.SpecialHandling
                                {
                                    MaxAmountPerPouch = 4,
                                    Narcotic = true,
                                    NeedsCooling = false,
                                    RobotHandling = false,
                                    SeperatePouch = false,
                                    Splitable = true
                                },
                                SynonymIds = new List<Model.Synonym>(),
                                TrayFillOnly = false,
                };
                var response = await SmartDoseServerWithHttpStatus
                                        .AppendPathSegment("Medicines")
                                        .PostJsonAsync(medicine);
                ResponseMessage(response.StatusCode, $"Medicine create {medicine.Name}");
            
                var medicines = await SmartDoseServer
                                        .AppendPathSegment("Medicines")
                                        .GetJsonAsync<List<Model.Customer>>();
                Information($"Customers={medicines.Count}");
        }).Wait();
    });

/*
Task("UpdateCustomer")
    .Does(()=> {
        System.Threading.Tasks.Task.Run(async ()=> {
            var customer = new Model.Customer
            {
                CustomerId = "4711",
                Name = "Name4711-"+ DateTime.Now.ToString(),
                Description= "Hallo",
            };
            var response = await SmartDoseServer
                                    .AppendPathSegments("Customers", customer.CustomerId)
                                    .PutJsonAsync(customer);
            ResponseMessage(response.StatusCode, $"Customer update {customer.Name}");
            
            var customers = await SmartDoseServer
                                    .AppendPathSegment("Customers")
                                    .GetJsonAsync<List<Model.Customer>>();
            Information($"Customers={customers.Count}");
        }).Wait();
    });
*/ 
#endregion


#region Ticket Helper

async Task CreateMedicine(Model.Medicine medicine)
{
    Information($"Create Medicine {medicine.Name}");
    try {
        var response = await SmartDoseServerWithHttpStatus
                                .AppendPathSegment("Medicines")
                                .PostJsonAsync(medicine).ConfigureAwait(false);
        ResponseMessage(response.StatusCode, "Medicine create");
    }
    catch(Exception ex)
    {
        Error(ex.ToString());
    }
}

async Task CreateMedicineFromExternalOrder(Model.ExternalOrder externalOrder)
{
    foreach(var orderDetail in externalOrder.OrderDetails)
        foreach(var intakeDetail in orderDetail.IntakeDetails)
            foreach(var medicationDetail in intakeDetail.MedicationDetails)
                await CreateMedicine(new Model.Medicine{
                                Active = true,
                                Comment = "Comment " + Guid.NewGuid().ToString(),
                                Description = "Med Desc " + Guid.NewGuid().ToString(),
                                Identifier = medicationDetail.MedicineId,
                                Name = medicationDetail.PrescribedMedicine,
                                Pictures = new List<Model.MedicinePicture>(),
                                PouchMode = Model.PouchMode.MultiDose,
                                PrintDetails = new List<Model.PrintDetail>(),
                                SpecialHandling = new Model.SpecialHandling
                                {
                                    MaxAmountPerPouch = 4,
                                    Narcotic = true,
                                    NeedsCooling = false,
                                    RobotHandling = false,
                                    SeperatePouch = false,
                                    Splitable = true
                                },
                                SynonymIds = new List<Model.Synonym>(),
                                TrayFillOnly = false,
                }).ConfigureAwait(false);
}

async Task CreateExternalOrder(string jsonFilename)
{
    var externalOrder= jsonFilename.FromJsonFile<Model.ExternalOrder>();
    Information($"Order {externalOrder.ExternalId}");
    await CreateMedicineFromExternalOrder(externalOrder).ConfigureAwait(false);
    var response = await SmartDoseServerWithHttpStatus
                                .AppendPathSegment("Orders")
                                .PostJsonAsync(externalOrder);
    ResponseMessage(response.StatusCode, $"Order create {externalOrder.ExternalId}");
}
#endregion

#region Tickets 

#region Ticket 1804

Task("Ticket-Sw-1804-Test")
    .Does(()=> {
        System.Threading.Tasks.Task.Run(async ()=> {
            await CreateExternalOrder("./Tickets/SW-1804/Test.json").ConfigureAwait(false);
    }).Wait();
});
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
