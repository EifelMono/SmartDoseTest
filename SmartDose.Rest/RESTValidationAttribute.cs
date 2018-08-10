using System;

namespace SmartDose.REST.Base.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RESTValidationAttribute : Attribute
    {
        public int MaxStringLength { get; set; } = -1;
        public bool NullAllowed { get; set; } = true;
        public bool IsDateTimeString { get; set; } = false;
        public bool IsMandatory { get; set; } = false;
    }
}