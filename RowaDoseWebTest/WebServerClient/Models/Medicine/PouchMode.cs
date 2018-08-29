using System;

namespace WebServerClient.Models.Medicine
{
    [Serializable]
    public enum PouchMode
    {
        /// <summary>
        ///     The multi dose.
        /// </summary>
        MultiDose,

        /// <summary>
        ///     The combi dose.
        /// </summary>
        CombiDose,

        /// <summary>
        ///     The unit ddose.
        /// </summary>
        UnitDdose,

        /// <summary>
        ///     The print only.
        /// </summary>
        PrintOnly,

        /// <summary>
        ///     The ignore.
        /// </summary>
        Ignore
    }
}