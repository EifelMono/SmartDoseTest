using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmartDose.Rest.Extensions
{
    public static class EmcExtensions
    {
        public static T JsonClone<T>(this T thisValue)
         => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(thisValue));

        public static string ToJsonString(this object thisValue, Formatting formating = Formatting.Indented)
            => JsonConvert.SerializeObject(thisValue, formating);

        public static string Dump(this object thisValue)
            => thisValue.ToJsonString(Formatting.Indented);

        public static string FileReadAllText(this string thisValue)
            => File.ReadAllText(thisValue);
        public static void FileWriteAllText(this string thisValue, string text)
            => File.WriteAllText(thisValue, text);

        public static T FileReadJson<T>(this string thisValue)
            => JsonConvert.DeserializeObject<T>(thisValue.FileReadAllText());
        public static void FileWriteJson<T>(this string thisValue, object data, Formatting formatting = Formatting.None)
            => thisValue.FileWriteAllText(JsonConvert.SerializeObject(data, formatting));

        public static T Pipe<T>(this T thisValue, Action<T> action)
        {
            action(thisValue);
            return thisValue;
        }

        public static T Pipe<T>(this T thisValue, Func<T, T> action)
            => action(thisValue);
    }
}
