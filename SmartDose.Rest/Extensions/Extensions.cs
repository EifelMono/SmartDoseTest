using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SmartDose.Rest.Extensions
{
    public static class Extensions
    {
        public static string ToRfId(this int thisValue) => $"{thisValue:00000000}";
        public static string ToRotorId(this int thisValue) => $"{thisValue:0000000}-{thisValue:00000}-{thisValue:00000}";
    }
}
