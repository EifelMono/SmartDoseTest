using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDose.RestCore.Helpers
{
    public static class Extensions
    {
        public static string ObjectToJson(this object value, Formatting formatting= Formatting.None)
            => JsonConvert.SerializeObject(value, formatting);

        public static object JsonToObject<T>(this string value)
            => JsonConvert.DeserializeObject<T>(value);

        public static string ToRfId(this int thisValue) => $"{thisValue:00000000}";
        public static string ToRotorId(this int thisValue) => $"{thisValue:0000000}-{thisValue:00000}-{thisValue:00000}";
    }
}
