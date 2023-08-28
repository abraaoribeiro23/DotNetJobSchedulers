namespace Infrastructure.Dkron.Common.Enums.Legacy
{
    public enum SqlServerJobFrequencyTypes
    {
        /// <summary>
        /// Runs for once
        /// </summary>
        Once = 1,

        /// <summary>
        /// Runs daily
        /// </summary>
        Daily = 4,

        /// <summary>
        /// Runs weekly
        /// </summary>
        Weekly = 8,

        /// <summary>
        /// Runs monthly on a specific date
        /// </summary>
        Monthly = 16,

        /// <summary>
        /// Run monthly on a specific pattern days (last Monday, second Tuesday, etc.)
        /// </summary>
        MonthlyRelative = 32,

        /// <summary>
        /// Bad Value
        /// </summary>
        Unknown = 255
    }
}