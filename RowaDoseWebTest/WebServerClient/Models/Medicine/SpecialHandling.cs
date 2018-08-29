using System;

namespace WebServerClient.Models.Medicine
{
    [Serializable]
    public class SpecialHandling
    {
        /// <summary>
        ///     Gets or sets a value indicating whether narcotic.
        /// </summary>
        public bool Narcotic { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether it is separate pouch.
        /// </summary>
        public bool SeperatePouch { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether robot handling.
        /// </summary>
        public bool RobotHandling { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether needs cooling.
        /// </summary>
        public bool NeedsCooling { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether splittable.
        /// </summary>
        public bool Splitable { get; set; }

        /// <summary>
        ///     Gets or sets the max amount per pouch. 0 no limit = 7  =>  0(7) => max <= 7
        /// </summary>
        public int MaxAmountPerPouch { get; set; }
    }
}