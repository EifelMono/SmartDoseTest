using SmartDose.Rest.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDose.Rest
{
    public static class Defaults
    {
        public static Models.Customer Customer(string id, string name = null)
            => new Models.Customer
            {
                ContactAddress = new Models.ContactAddress
                {
                    AddressLine1 = $"Addressline1 {id}",
                    City = $"City {id}",
                    Country = $"Country {id}",
                    NameLine1 = $"NameLine1 {id}",
                    Postalcode = $"Postalcode {id}",
                    State = $"State {id}",
                },
                ContactPerson = new Models.ContactPerson
                {
                    Email = $"Email {id}",
                    Name = $"Name {id}",
                    TelephoneNumber = $"TelephoneNumber {id}"
                },
                CustomerId = id,
                Description = $"Description {id}",
                Fax = $"Fax {id}",
                Name = name ?? $"Name {id}",
                Website = $"WebSite {id}",
            };
        public static Models.Canister Canister(int id)
          => new Models.Canister
          {
              CanisterId = id.ToString(),
              Rfid = id.ToRfId(),
              Largecanister = false,
              RotorId = id.ToRotorId(),
          };

        public static Models.Medicine Medicine(string id, string name = null)
            => new Models.Medicine
            {
                Active = true,
                Comment = $"Comment {id}",
                Description = $"Med Desc {id}",
                MedicineId = id,
                Name = name ?? $"Medicine {id}",
                Pictures = new List<Models.MedicinePicture>(),
                PouchMode = Models.PouchMode.MultiDose,
                PrintDetails = new List<Models.PrintDetail>(),
                SpecialHandling = new Models.SpecialHandling
                {
                    MaxAmountPerPouch = 4,
                    Narcotic = true,
                    NeedsCooling = false,
                    RobotHandling = false,
                    SeperatePouch = false,
                    Splitable = true
                },
                SynonymIds = new List<Models.Synonym>(),
                TrayFillOnly = false,
            };
    }
}
