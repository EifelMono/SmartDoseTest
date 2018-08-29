using System;

namespace WebServerClient.Models.Medicine
{
    [Serializable]
    public class PrintDetail
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the intake advice.
        /// </summary>
        public string IntakeAdvice { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        ///     Gets or sets the form type.
        /// </summary>
        public string FormType { get; set; }

        /// <summary>
        ///     Gets or sets the pill type.
        /// </summary>
        public string PillType { get; set; }

        /// <summary>
        ///     Gets or sets the generic name.
        /// </summary>
        public string GenericName { get; set; }

        /// <summary>
        ///     Gets or sets the additional advice.
        /// </summary>
        public string AdditionalAdvice { get; set; }

        /// <summary>
        ///     Gets or sets the medication class.
        /// </summary>
        public string MedicationClass { get; set; }
    }
}