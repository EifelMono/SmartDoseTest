using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDose.RestCore.Helpers
{
    public static class Tools
    {
        public static async void x()
        {
            dynamic x = await "http://localhost:6040/SmartDose/Customers".GetJsonAsync();
        }
    }
}
