using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;
using WebServerClient.Helpers;
using WebServerClient.Models;
using WebServerClient.Models.Canister;
using WebServerClient.Models.Customer;
using WebServerClient.Models.ExternalOrder;
using WebServerClient.Models.Manufacturer;
using WebServerClient.Models.Medicine;
using WebServerClient.Models.Order;

namespace WebServerClient
{
    public partial class Form1 : Form
    {
        /// <summary>
        ///     Holds the value of the IP address;
        /// </summary>
        private string _ipAddress = string.Empty;

        /// <summary>
        ///     Holds the value of the port
        /// </summary>
        private string _port = string.Empty;

        public Form1()
        {
            InitializeComponent();
            setting_save_button.Enabled = false;
            ip_textbox.Text = "127.0.0.1";
            port_textbox.Text = "6042";
            setting_save_button_Click(null, null);
        }

        /// <summary>
        ///     Formated string to be used by the HTTP requests.
        /// </summary>
        private string Webserver
        {
            get { return $"http://{_ipAddress}:{_port}/smartdose"; }
        }

        /// <summary>
        ///     Saves the current settings for the web server
        /// </summary>
        private void setting_save_button_Click(object sender, EventArgs e)
        {
            _ipAddress = ip_textbox.Text;
            _port = port_textbox.Text;
            setting_save_button.Enabled = false;
           // show_confirmation_label.ShowConfirmation();
            groupBox2.Enabled = true;
        }

        /// <summary>
        ///     Validates the IP address
        /// </summary>
        private void ip_textbox_KeyUp(object sender, KeyEventArgs e)
        {
            ValidateEntries();
        }

        /// <summary>
        ///     Validates the port to be a valid integer.
        /// </summary>
        private void port_textbox_KeyUp(object sender, KeyEventArgs e)
        {
            ValidateEntries();
        }

        #region private 

        /// <summary>
        ///     Checks all entries for validity.
        /// </summary>
        private void ValidateEntries()
        {
            if (ip_textbox.Text.IsValidIp() && port_textbox.Text.IsValidPort())
                setting_save_button.Enabled = true;
        }

        /// <summary>
        ///     Performs a get from the smart dose server
        /// </summary>
        /// <param name="url">The URL to call to.</param>
        /// <returns>Json formated result</returns>
        private string GetFromServer(string url)
        {
            var time = Stopwatch.StartNew();
            try
            {
                json_textbox.AppendResponse("Retrieving...");
                // Create a request for the URL.   
                var request = WebRequest.Create(url);
                // If required by the server, set the credentials.  
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.  
                var response = request.GetResponse();
                // Get the stream containing content returned by the server.  
                var dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.  
                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);
                    // Read the content.  
                    var data = reader.ReadToEnd();

                    // Clean up the streams and the response. 
                    reader.Close();
                    response.Close();

                    // json_textbox.AppendText(data.FormatJson());

                    json_textbox.AppendText($"time {time.Elapsed}");

                    return data;
                }
                
                json_textbox.AppendText("No data");

                return string.Empty;
            }
            catch (WebException ex)
            {
                json_textbox.AppendText(ex.Message);
                return string.Empty;
            }
        }


        private void PostToServer(string url, object data)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(data));
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    json_textbox.AppendResponse(result);
                }
            }
            catch (HttpRequestException ex)
            {
                json_textbox.AppendResponse(ex.Message);
            }
            catch (WebException ex)
            {
                json_textbox.AppendResponse(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        private void get_all_medicine_button_Click(object sender, EventArgs e)
        {
            var responseFromServer = GetFromServer($"{Webserver}/Medicines/");

            /*
             *  Display the content. (application model), Your app !!!
             */
            var medicines = JsonConvert.DeserializeObject<List<Medicine>>(responseFromServer);

            //// Shows the response in the text box for debugging purposes.
            //Invoke((MethodInvoker) delegate
            //{
            //    json_textbox.Text = string.Empty;
            //    json_textbox.Text = responseFromServer.FormatJson();
            //});
        }

        /// <summary>
        /// </summary>
        private void get_all_manufacturer_button_Click(object sender, EventArgs e)
        {
            var responseFromServer = GetFromServer($"{Webserver}/Medicines/16258721000171111");

            /*
             *  Display the content. (application model), Your app !!!
             */
            var medicines = JsonConvert.DeserializeObject<Medicine>(responseFromServer);

            //// Shows the response in the text box for debugging purposes.
            //Invoke((MethodInvoker) delegate
            //{
            //    json_textbox.Text = string.Empty;
            //    json_textbox.Text = responseFromServer.FormatJson();
            //});
        }

        /// <summary>
        /// </summary>
        private void get_all_orders_button_Click(object sender, EventArgs e)
        {
            var responseFromServer = GetFromServer($"{Webserver}/Orders/123");

            /*
             *  Display the content. (application model), Your app !!!
             */
            var orders = JsonConvert.DeserializeObject<OrderDetail>(responseFromServer);

            //// Shows the response in the text box for debugging purposes.
            Invoke((MethodInvoker)delegate
           {
               json_textbox.Text = string.Empty;
               json_textbox.Text = responseFromServer.FormatJson();
           });
        }

        /// <summary>
        /// </summary>
        private void get_all_canister_button_Click(object sender, EventArgs e)
        {
            var responseFromServer = GetFromServer($"{Webserver}/Canisters/");

            /*
             *  Display the content. (application model), Your app !!!
             */
            var canisters = JsonConvert.DeserializeObject<List<OrderResult>>(responseFromServer);

            //// Shows the response in the text box for debugging purposes.
            //Invoke((MethodInvoker) delegate
            //{
            //    json_textbox.Text = string.Empty;
            //    json_textbox.Text = responseFromServer.FormatJson();
            //});
        }

        /// <summary>
        ///     Adds a random medicine.
        /// </summary>
        private void add_medicine_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10000; ++i)
            {
                var medicine = new Medicine
                {
                    Active = true,
                    Comment = "Comment " + Guid.NewGuid().ToString(),
                    Description = "Med Desc " + Guid.NewGuid().ToString(),
                    Identifier = Guid.NewGuid().ToString(),
                    Name = "Med Name " + Guid.NewGuid().ToString(),
                    Pictures = new List<MedicinePicture>(),
                    PouchMode = PouchMode.MultiDose,
                    PrintDetails = new List<PrintDetail>(),
                    SpecialHandling = new SpecialHandling
                    {
                        MaxAmountPerPouch = 4,
                        Narcotic = true,
                        NeedsCooling = false,
                        RobotHandling = false,
                        SeperatePouch = false,
                        Splitable = true
                    },
                    SynonymIds = new List<Synonym>(),
                    TrayFillOnly = false,

                };

                PostToServer($"{Webserver}/Medicines/", medicine);
                this.Text = "Medicine " + i;
                this.Refresh();
            }
            
        }

        /// <summary>
        /// </summary>
        private void add_manufacturer_button_Click(object sender, EventArgs e)
        {
            var manufacturer = new Manufacturer
            {
                Name = Guid.NewGuid().ToString(),
                Identifier = Guid.NewGuid().ToString(),
                Comment = Guid.NewGuid().ToString(),
                Address = new ContactAddress()
            };

            PostToServer($"{Webserver}/manufacturer/", manufacturer);
        }

        /// <summary>
        /// </summary>
        private void add_order_button_Click(object sender, EventArgs e)
        {
            var order = new ExternalOrder();
            var tick = DateTime.Now.Ticks;
            /*
           * Steps to reproduce inserting an order in to the system.
           * 
           * 1. Create Customer if not existing
           * 2. Create medication if not existing,
           * 3. Make sure that the medication is released and can be used for production
           * 4. Create the order and assign intake details. 
           */

            //PostToServer($"{Webserver}/customers/", GetUniqueCustomer());
            var customers = JsonConvert.DeserializeObject<List<Customer>>(GetFromServer($"{Webserver}/customers/"));
            var customer = customers.First();
           // customer.CustomerId = "453436";
            //14714 - 13829
            //var custFromServer = JsonConvert.DeserializeObject<List<Customer>>(GetFromServer($"{Webserver}/customers/")).ToArray()[1];

            //PostToServer($"{Webserver}/medicines/", GetUniqueMedicine());
            var serverMedicine = JsonConvert.DeserializeObject<Medicine>(GetFromServer($"{Webserver}/medicines/794921000171115"));

            var destinationFacility = new DestinationFacility()
            {
                CustomerId = "0",
                DepartmentName = "Destination Facility",
                Name = "81bdd4e4-e378-4cab-925b-2fb5e1d1484e",
                ContactAddress = new ContactAddress()
                {
                    Addressline1 = "Straße",
                    City = "Stadt",
                    Country = "Germany",
                    Postalcode = "123456",
                },
                ContactPerson = new ContactPerson()
                {
                    TelephoneNumber = "12345678"
                }
            };

            var pharmacy = new Customer()
            {
                CustomerId = "APO-123456789-XXX",
                Name = "Apotheke",
                ContactAddress = new ContactAddress()
                {
                    Addressline1 = "Straße",
                    City = "Stadt",
                    Country = "Germany",
                    Postalcode = "123456",
                },
                ContactPerson = new ContactPerson()
                {
                    TelephoneNumber = "12345678"
                }
            };

            var patient = new Patient()
            {
                ContactAddress = new ContactAddress()
                {
                    NameLine1 = "Ms Helen Bradley",
                    Addressline1 = "115 Kingsway South, Warrington, Cheshire",
                    City = "Warrington",
                    Country = "Cheshire",
                    Postalcode = "WA4 1RW",
                },
                ContactPerson = new ContactPerson()
                {
                    Name = "Ms Helen Bradley",
                },
                RoomNumber = "",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(1944, 10, 08).ToString(),
                BedNumber = "",
                ExternalPatientNumber = "2100"
            };


            order.Customer = customer;
            order.ExternalId = "Helens Order " + Environment.TickCount.ToString();
            order.State = OrderState.Undefined;
            order.Timestamp = DateTime.Now.Ticks.ToString();
            order.OrderDetails = new List<OrderDetail>()
            {
                new OrderDetail()
                {
                    DestinationFacility = destinationFacility,
                    Pharmacy = pharmacy,
                    Patient = patient,
                    IntakeDetails = new List<IntakeDetail>()
                    {
                        new IntakeDetail()
                        {
                            IntakeDateTime = DateTime.Now.AddDays(1).ToString(),
                            MedicationDetails = new List<MedicationDetail>()
                            {
                                new MedicationDetail()
                                {
                                    MedicineId = serverMedicine.Identifier,
                                    Physician = "DR Doolittle",
                                    IntakeAdvice = "Take one",
                                    Count = 2
                                }
                            }

                        },
                        new IntakeDetail()
                        {
                            IntakeDateTime = DateTime.Now.AddDays(2).ToString(),
                            MedicationDetails = new List<MedicationDetail>()
                            {
                                new MedicationDetail()
                                {
                                    MedicineId = serverMedicine.Identifier,
                                    Physician = "DR Doolittle",
                                    IntakeAdvice = "Take one",
                                    Count = 2
                                }
                            }

                        },
                        new IntakeDetail()
                        {
                            IntakeDateTime = DateTime.Now.AddDays(3).ToString(),
                            MedicationDetails = new List<MedicationDetail>()
                            {
                                new MedicationDetail()
                                {
                                    MedicineId = serverMedicine.Identifier,
                                    Physician = "DR Doolittle",
                                    IntakeAdvice = "Take one",
                                    Count = 2
                                }
                            }

                        },
                        new IntakeDetail()
                        {
                            IntakeDateTime = DateTime.Now.AddDays(4).ToString(),
                            MedicationDetails = new List<MedicationDetail>()
                            {
                                new MedicationDetail()
                                {
                                    MedicineId = serverMedicine.Identifier,
                                    Physician = "DR Doolittle",
                                    IntakeAdvice = "Take one",
                                    Count = 2
                                }
                            }

                        }

                    }
                }
            };


          

            #region 

            //{
            //    ExternalId = Guid.NewGuid().ToString().Replace("-",""),
            //    State = OrderState.Undefined,
            //    Timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),
            //    Customer = new Customer
            //    {
            //        Name = Guid.NewGuid().ToString(),
            //        Description = Guid.NewGuid().ToString(),
            //        CustomerId = Guid.NewGuid().ToString(),
            //        Fax = Guid.NewGuid().ToString(),
            //        Website = Guid.NewGuid().ToString(),
            //        ContactPerson = new ContactPerson
            //        {
            //            Name = Guid.NewGuid().ToString(),
            //            Email = Guid.NewGuid().ToString(),
            //            TelephoneNumber = Guid.NewGuid().ToString()
            //        },
            //        ContactAddress = new ContactAddress
            //        {
            //            State = Guid.NewGuid().ToString(),
            //            Addressline1 = Guid.NewGuid().ToString(),
            //            City = Guid.NewGuid().ToString(),
            //            Country = Guid.NewGuid().ToString(),
            //            NameLine1 = Guid.NewGuid().ToString(),
            //            Postalcode = Guid.NewGuid().ToString()
            //        }
            //    },
            //    OrderDetails = new List<OrderDetail>
            //    {
            //        new OrderDetail
            //        {
            //            DestinationFacility = new DestinationFacility
            //            {
            //                Name = Guid.NewGuid().ToString(),
            //                CustomerId = Guid.NewGuid().ToString(),
            //                DepartmentName = Guid.NewGuid().ToString(),
            //                DepartmentCode = Guid.NewGuid().ToString(),
            //                ContactPerson = new ContactPerson
            //                {
            //                    Name = Guid.NewGuid().ToString(),
            //                    Email = Guid.NewGuid().ToString(),
            //                    TelephoneNumber = Guid.NewGuid().ToString()
            //                },
            //                ContactAddress = new ContactAddress
            //                {
            //                    State = Guid.NewGuid().ToString(),
            //                    Addressline1 = Guid.NewGuid().ToString(),
            //                    City = Guid.NewGuid().ToString(),
            //                    Country = Guid.NewGuid().ToString(),
            //                    NameLine1 = Guid.NewGuid().ToString(),
            //                    Postalcode = Guid.NewGuid().ToString()
            //                }
            //            },
            //            Pharmacy = new Customer
            //            {
            //                Name = Guid.NewGuid().ToString(),
            //                CustomerId = Guid.NewGuid().ToString(),
            //                Website = Guid.NewGuid().ToString(),
            //                ContactPerson = new ContactPerson
            //                {
            //                    Name = Guid.NewGuid().ToString(),
            //                    Email = Guid.NewGuid().ToString(),
            //                    TelephoneNumber = Guid.NewGuid().ToString()
            //                },
            //                ContactAddress = new ContactAddress
            //                {
            //                    State = Guid.NewGuid().ToString(),
            //                    Addressline1 = Guid.NewGuid().ToString(),
            //                    City = Guid.NewGuid().ToString(),
            //                    Country = Guid.NewGuid().ToString(),
            //                    NameLine1 = Guid.NewGuid().ToString(),
            //                    Postalcode = Guid.NewGuid().ToString()
            //                }
            //            },
            //            Patient = new Patient
            //            {
            //                BedNumber = "505",
            //                DateOfBirth = "yesterday",
            //                Gender = Gender.Male,
            //                WardName = "Ward",
            //                ContactPerson = new ContactPerson
            //                {
            //                    Name = Guid.NewGuid().ToString(),
            //                    Email = Guid.NewGuid().ToString(),
            //                    TelephoneNumber = Guid.NewGuid().ToString()
            //                },
            //                ContactAddress = new ContactAddress
            //                {
            //                    State = Guid.NewGuid().ToString(),
            //                    Addressline1 = Guid.NewGuid().ToString(),
            //                    City = Guid.NewGuid().ToString(),
            //                    Country = Guid.NewGuid().ToString(),
            //                    NameLine1 = Guid.NewGuid().ToString(),
            //                    Postalcode = Guid.NewGuid().ToString()
            //                }
            //            },
            //            IntakeDetails = new List<IntakeDetail>
            //            {
            //                new IntakeDetail
            //                {
            //                    IntakeDateTime = DateTime.Now.AddDays(1D).ToString(),
            //                    MedicationDetails = new List<MedicationDetail>
            //                    {
            //                        new MedicationDetail
            //                        {
            //                            Count = 1,
            //                            IntakeAdvice = "Take one",
            //                            MedicineId = "medID",
            //                            PhysicianComment = "Comment",
            //                            Physician = "Dr Doolittle"
            //                        },
            //                        new MedicationDetail
            //                        {
            //                            Count = 1,
            //                            IntakeAdvice = "Take one",
            //                            MedicineId = "medID",
            //                            PhysicianComment = "Comment",
            //                            Physician = "Dr Doolittle"
            //                        },
            //                        new MedicationDetail
            //                        {
            //                            Count = 1,
            //                            IntakeAdvice = "Take one",
            //                            MedicineId = "medID",
            //                            PhysicianComment = "Comment",
            //                            Physician = "Dr Doolittle"
            //                        }
            //                    }
            //                }
            //            }
            //        },
            //        new OrderDetail
            //        {
            //            DestinationFacility = new DestinationFacility
            //            {
            //                Name = Guid.NewGuid().ToString(),
            //                CustomerId = Guid.NewGuid().ToString(),
            //                DepartmentName = Guid.NewGuid().ToString(),
            //                DepartmentCode = Guid.NewGuid().ToString(),
            //                ContactPerson = new ContactPerson
            //                {
            //                    Name = Guid.NewGuid().ToString(),
            //                    Email = Guid.NewGuid().ToString(),
            //                    TelephoneNumber = Guid.NewGuid().ToString()
            //                },
            //                ContactAddress = new ContactAddress
            //                {
            //                    State = Guid.NewGuid().ToString(),
            //                    Addressline1 = Guid.NewGuid().ToString(),
            //                    City = Guid.NewGuid().ToString(),
            //                    Country = Guid.NewGuid().ToString(),
            //                    NameLine1 = Guid.NewGuid().ToString(),
            //                    Postalcode = Guid.NewGuid().ToString()
            //                }
            //            },
            //            Pharmacy = new Customer
            //            {
            //                Name = Guid.NewGuid().ToString(),
            //                CustomerId = Guid.NewGuid().ToString(),
            //                Website = Guid.NewGuid().ToString(),
            //                ContactPerson = new ContactPerson
            //                {
            //                    Name = Guid.NewGuid().ToString(),
            //                    Email = Guid.NewGuid().ToString(),
            //                    TelephoneNumber = Guid.NewGuid().ToString()
            //                },
            //                ContactAddress = new ContactAddress
            //                {
            //                    State = Guid.NewGuid().ToString(),
            //                    Addressline1 = Guid.NewGuid().ToString(),
            //                    City = Guid.NewGuid().ToString(),
            //                    Country = Guid.NewGuid().ToString(),
            //                    NameLine1 = Guid.NewGuid().ToString(),
            //                    Postalcode = Guid.NewGuid().ToString()
            //                }
            //            },
            //            Patient = new Patient
            //            {
            //                BedNumber = "505",
            //                DateOfBirth = "yesterday",
            //                Gender = Gender.Male,
            //                WardName = "Ward"
            //            }
            //        }
            //    }
            //};

            #endregion

            PostToServer($"{Webserver}/Orders/", order);

            //var orders = JsonConvert.DeserializeObject<List<OrderStatus>>(GetFromServer($"{Webserver}/Orders/"));
            //var x = 1;
        }

        private Medicine GetUniqueMedicine()
        {
            var tick = DateTime.Now.Ticks.ToString();

            var medicine = new Medicine()
            {
                Name = $"Medicine {tick}",
                Description = "Description",
                Identifier = tick,
                Comment = "Comment",
                Active = true, // Already active
                PrintDetails = new List<PrintDetail>(),
                TrayFillOnly = false,
                Pictures = new List<MedicinePicture>(),
                PouchMode = PouchMode.CombiDose,
                SpecialHandling = new SpecialHandling() { MaxAmountPerPouch = 7},
                SynonymIds = new List<Synonym>()
            };

            return medicine;
        }

        private DestinationFacility GetUniqueDest()
        {
             var destFacility = new DestinationFacility()
             {
                 ContactAddress = GetUniqueContactAddress(),
                 ContactPerson = GetUniqueContactPerson(),
                 Name = $"Name {Guid.NewGuid().ToString().Substring(0, 4)}",
                 CustomerId = "",
                 DepartmentCode = "Dep code",
                 DepartmentName = "Dep name"
             };

            return destFacility;
        }

        private Customer GetUniqueCustomer()
        {
            var customer = new Customer()
            {
                Name = "Customer",
                CustomerId = $"RX{DateTime.Now.Ticks}",
                Fax = "FAX Number",
                Website = "Website",
                Description = "Just the description ", // Mandatory
                ContactAddress = GetUniqueContactAddress(),
                ContactPerson = GetUniqueContactPerson()
            };

            return customer;
        }


        private ContactAddress GetUniqueContactAddress()
        {
            var contactAddress = new ContactAddress()
            {
                State = "Your state",
                Addressline1 = "My first address",
                City = "This city",
                Postalcode = "9897952",
                NameLine1 = $"First Name {Guid.NewGuid().ToString().Substring(0,4)}",
                Country = "This country"
            };
            return contactAddress;
        }

        private ContactPerson GetUniqueContactPerson()
        {
            var contactP = new ContactPerson()
            {
                Name = "Name of the person",
                Email = "Email of the person",
                TelephoneNumber = "Phone Number"
            };

            return contactP;
        }

        /// <summary>
        /// </summary>
        private void add_canister_button_Click(object sender, EventArgs e)
        {
            var canister = new Canister
            {
                CanisterId = Guid.NewGuid().ToString(),
                Largecanister = true,
                Rfid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8),
                RotorId = "rotor"
            };
            PostToServer($"{Webserver}/Canisters/", canister);
        }

        #endregion
    }
}