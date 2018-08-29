using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace TimeTest
{
    class Program
    {
        static string WebServer => $"http://localhost:6040/smartdose";

        static string GetFromServer(string url, string search= "", string countOccurence= null)
        {
            var time = Stopwatch.StartNew();
            try
            {
                if (!string.IsNullOrEmpty(search))
                {
                    if (!search.StartsWith("all"))
                        url += $"/{search}";
                }
                Console.WriteLine($"Retrieving... {url}");
                var request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                var response = request.GetResponse();
                Console.WriteLine($"time response {time.Elapsed} ");
                var dataStream = response.GetResponseStream();
                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);
                    var data = reader.ReadToEnd();
                    reader.Close();
                    response.Close();
                    Console.WriteLine($"time read {time.Elapsed} ");
                    if (!string.IsNullOrEmpty(countOccurence))
                        Console.WriteLine($"time count {time.Elapsed}, elements {Regex.Matches(data, countOccurence).Count}");
                    Console.WriteLine();
                    return data;
                }
                Console.WriteLine("No data");

                return string.Empty;
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello SmartDose!");
            GetFromServer("http://localhost:6040/smartdose/medicines", "tictacwhite", "TrayFillOnly");
            GetFromServer("http://localhost:6040/smartdose/medicines", "1", "TrayFillOnly");
            GetFromServer("http://localhost:6040/smartdose/medicines", "all1", "TrayFillOnly");
            GetFromServer("http://localhost:6040/smartdose/medicines", "16258721000171111", "TrayFillOnly");
            GetFromServer("http://localhost:6040/smartdose/medicines", "all2", "TrayFillOnly");
            Console.WriteLine("Ready!");
            Console.ReadLine();
        }
    }
}
