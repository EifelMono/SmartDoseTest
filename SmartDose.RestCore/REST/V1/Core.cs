using Flurl;
using FlurlHttp;

namespace SmartDose.RestCore.REST.V1
{
    public static class Core
    {
        public static string Url { get; set; } = "http://localhost:6040/smartdose";
        public static string AcceptedStatusCodes => "0-999";

    }
}