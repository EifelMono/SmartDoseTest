using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Globalization;

namespace SmartDose.Rest
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RestValidationAttribute : Attribute
    {
        public int MaxStringLength { get; set; } = -1;
        public bool NullAllowed { get; set; } = true;
        public bool CheckDateTimeString { get; set; } = false;
    }

    /// <summary>
    /// RestValidation class 
    /// </summary>
    public static class RestValidation
    {
        /// <summary>
        /// Enable or disable the RestValidation Check
        /// by default the check is on ...
        /// </summary>
        public static bool Enabled { get; set; } = true;

        /// <summary>
        /// Test function to check the property of the rootObject and childs objects
        /// againt the RestValidationAttributes.
        /// if there is an error Ok is false and Infos contains 
        /// the wrong elments by path name and info about the error
        /// </summary>
        /// <param name="rootObject"></param>
        /// <param name="rootName"></param>
        /// <returns></returns>
        public static (bool Ok, string Infos) Check(object rootObject, string rootName)
        {
            if (!Enabled)
                return (true, "");

            var sb = new StringBuilder();
            try
            {
                InternallCheck(rootObject, rootName);
            }
            catch (Exception ex)
            {
                return (false, ex.ToString());
            }
            return (sb.Length is 0, sb.ToString());


            void InternallCheck(object detailObject, string detailName)
            {
                if (detailObject is null)
                    return;
                foreach (var property in detailObject.GetType().GetProperties())
                {
                    var propertyName = property.Name;
                    var propertyValue = property.GetValue(detailObject);
                    if (propertyValue != null)
                    {
                        var propertyType = propertyValue.GetType();
                        switch (Type.GetTypeCode(propertyType))
                        {
                            case TypeCode.String:
                                {
                                    if (property.GetRestValidationAttribute() is var restValidationAttribute && restValidationAttribute != null)
                                    {
                                        if (restValidationAttribute.MaxStringLength != -1)
                                        {
                                            var text = (string)propertyValue;
                                            if (text.Length > restValidationAttribute.MaxStringLength)
                                                sb.AppendLine($"{detailName}.{propertyName} wrong length [{text.Length}>{restValidationAttribute.MaxStringLength}]");
                                        }
                                        if (restValidationAttribute.CheckDateTimeString)
                                        {
                                            var text = (string)propertyValue;
                                            if (!DateTime.TryParseExact(text, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                                                sb.AppendLine($"{detailName}.{propertyName} wrong DateTime format \"{text}\"");
                                        }
                                    }
                                    break;
                                }
                            case TypeCode.Object:
                                {
                                    if (propertyType.IsArray)
                                    {
                                        Array arrayPropertyValues = (Array)propertyValue;
                                        for (int i = 0; i < arrayPropertyValues.Length; i++)
                                            InternallCheck(arrayPropertyValues.GetValue(i), $"{detailName}.{propertyName}[{i}]");
                                    }
                                    else if (propertyValue is IEnumerable)
                                    {
                                        IEnumerable listPropertyValues = (IEnumerable)propertyValue;
                                        var i = 0;
                                        foreach (var listPropertyValue in listPropertyValues)
                                        {
                                            InternallCheck(listPropertyValue, $"{detailName}.{propertyName}[{i}]");
                                            i++;
                                        }
                                    }
                                    else
                                        InternallCheck(propertyValue, $"{detailName}.{propertyName}");
                                    break;
                                }
                        }
                    }
                    else
                    {
                        if (property.GetRestValidationAttribute() is var restValidationAttribute && restValidationAttribute != null && !restValidationAttribute.NullAllowed)
                            sb.AppendLine($"{detailName}.{propertyName} null value not allowed");
                    }
                }
            }
        }

        private static RestValidationAttribute GetRestValidationAttribute(this PropertyInfo thisValue)
            => (RestValidationAttribute)thisValue.GetCustomAttributes(typeof(RestValidationAttribute), false).FirstOrDefault();
    }
}
