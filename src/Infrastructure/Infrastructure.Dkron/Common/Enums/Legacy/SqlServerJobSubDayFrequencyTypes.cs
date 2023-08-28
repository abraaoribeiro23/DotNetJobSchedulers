namespace Infrastructure.Dkron.Common.Enums.Legacy
{
    public enum SqlServerJobSubDayFrequencyTypes
    {
        /// <summary>
        /// Runs for once
        /// </summary>
        SpecifiedTime = 1,

        /// <summary>
        /// Runs at seconds-level frequency
        /// </summary>
        Seconds = 2,

        /// <summary>
        /// Runs at minute-level frequency
        /// </summary>
        Minutes = 4,

        /// <summary>
        /// Runs at hourly frequency
        /// </summary>
        Hours = 8,

        /// <summary>
        /// Bad Value
        /// </summary>
        Unknown = 255
    }
}