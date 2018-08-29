using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace SmartDose.Rest.Base.Validation
{

    public static class MedicineChecker
    {
        public static string GetFromServer(string url)
        {
            try
            {
                var request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                var response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);
                    var data = reader.ReadToEnd();
                    reader.Close();
                    response.Close();
                    return data;
                }
                return string.Empty;
            }
            catch (WebException ex)
            {
                return string.Empty;
            }
        }

        public async static Task<bool> CheckMedicineAsync(string medicine)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var x = GetFromServer($"http://localhost:6040/SmartDose/medicines/{medicine}");
                    return !String.IsNullOrEmpty(x);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return false;
                }
            }).ConfigureAwait(false);
        }

        public async static Task<bool> CheckMedicinesAsync(List<string> medicines)
        {
            List<Task<bool>> tasks = new List<Task<bool>>();
            foreach (var medicine in medicines)
                tasks.Add(CheckMedicineAsync(medicine));
            await Task.WhenAll(tasks).ConfigureAwait(false);
            var result = true;
            foreach (var task in tasks)
                result = result & task.Result;
            return result;
        }
    }

    public class MedicineValidationAttribute : ValidationAttribute
    {
        public MedicineValidationAttribute(string errorMessage, bool optional = false) : base(errorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            var result = true;
            try
            {
                var x = MedicineChecker.GetFromServer($"http://localhost:6040/SmartDose/medicines/{value}");
                result = !String.IsNullOrEmpty(x);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                result = false;
            }
            return result;
        }
    }
}
