using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WebServerClient.Helpers
{
    public static class Extensions
    {
        public static bool IsValidIp(this string text)
        {
            IPAddress test;
            return IPAddress.TryParse(text, out test);
        }

        public static bool IsValidPort(this string text)
        {
            int value;
            return int.TryParse(text, out value);
        }

        public static void ShowConfirmation(this Control control)
        {
            control.Invoke((MethodInvoker) delegate { control.Visible = true; });
            var t = new Timer
            {
                Interval = 3000
            };
            //Hide confirmation after 3 seconds
            t.Tick += (s, e) =>
            {
                control.Visible = false;
                t.Stop();
            };
            t.Start();
        }


        public static void AppendResponse(this TextBox control, string text)
        {
            control.Invoke((MethodInvoker) delegate
            {
                control.AppendText(System.Environment.NewLine);
                control.AppendText($"=> {text}");
                control.AppendText(System.Environment.NewLine);
            });
        }

        public static string FormatJson(this string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
    }
}