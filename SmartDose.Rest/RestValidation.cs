using System;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Globalization;

namespace SmartDose.REST.Base.Validation
{
    /// <summary>
    /// RestValidation class 
    /// </summary>
    public static class RESTValidation
    {
        /// <summary>
        /// ENABLE or DISABLE the RestValidation Check 
        /// This is FOR ALL Checks !!!!!!!!!!!!!!!!!!!
        /// by default the check is on ...
        /// </summary>
        public static bool Enabled { get; set; } = true;

        /// <summary>
        /// Test function to check the properties of the root and childs
        /// objects against the RestValidationAttributes.
        /// If there is an error
        /// Ok flag is false and Infos contains 
        /// the wrong elments by path name and infos about the error
        /// Also the default HttpStatusCode 412 HttpStatusCode.PreconditionFailed
        /// is assigned in the StatusCode
        /// </summary>
        /// <param name="rootObject"></param>
        /// <param name="rootName"></param>
        /// <returns></returns>
        public static RESTValidationResult Check(object rootObject, string rootName)
        {
            if (!Enabled)
                return new RESTValidationResult(true, "");

            var sb = new StringBuilder();
            try
            {
                InternallCheck(rootObject, rootName);
            }
            catch (Exception ex)
            {
                return new RESTValidationResult(false, ex.ToString());
            }
            return new RESTValidationResult(sb.Length is 0, sb.ToString());


            void InternallCheck(object detailObject, string detailName)
            {
                if (detailObject is null)
                    return;
                foreach (var property in detailObject.GetType().GetProperties())
                {
                    var propertyName = property.Name;
                    var propertyValue = property.GetValue(detailObject);
                    var propertyAttributeRESTValidation= (RESTValidationAttribute)property.GetCustomAttributes(typeof(RESTValidationAttribute), false).FirstOrDefault();
                    if (propertyValue != null)
                    {
                        var propertyType = propertyValue.GetType();
                        switch (Type.GetTypeCode(propertyType))
                        {
                            case TypeCode.String:
                                {
                                    if (propertyAttributeRESTValidation != null)
                                    {
                                        var text = (string)propertyValue;
                                        if (propertyAttributeRESTValidation.MaxStringLength != -1)
                                        {
                                            if (text.Length > propertyAttributeRESTValidation.MaxStringLength)
                                                sb.AppendLine($"{detailName}.{propertyName} has a wrong length [{text.Length}>{propertyAttributeRESTValidation.MaxStringLength}]");
                                        }
                                        if (propertyAttributeRESTValidation.IsDateTimeString)
                                        {
                                            if (!DateTime.TryParseExact(text, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                                                sb.AppendLine($"{detailName}.{propertyName} has a wrong DateTime format \"{text}\"");
                                        }
                                        if (propertyAttributeRESTValidation.IsMandatory)
                                        {
                                            if (string.IsNullOrEmpty(text))
                                                sb.AppendLine($"{detailName}.{propertyName} this is a mandatory field an this is empty");
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
                        if (propertyAttributeRESTValidation != null && !propertyAttributeRESTValidation.NullAllowed)
                            sb.AppendLine($"{detailName}.{propertyName} null value not allowed");
                    }
                }
            }
        }
    }
}
