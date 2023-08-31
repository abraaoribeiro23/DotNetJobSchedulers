namespace Infrastructure.Dkron.Common.Enums.Legacy
{
    public enum JobWeeklyFrequencyIntervalTypes
    {
        /// <summary>
        /// Runs on Sunday
        /// </summary>
        Sunday = 1,

        /// <summary>
        /// Runs on Monday
        /// </summary>
        Monday = 2,

        /// <summary>
        /// Runs on Tuesday
        /// </summary>
        Tuesday = 4,

        /// <summary>
        /// Runs on Wednesday
        /// </summary>
        Wednesday = 8,

        /// <summary>
        /// Runs on Thursday
        /// </summary>
        Thursday = 16,

        /// <summary>
        /// Runs on Friday
        /// </summary>
        Friday = 32,

        /// <summary>
        /// Runs on Saturday
        /// </summary>
        Saturday = 64 ,

        /// <summary>
        /// Bad Value
        /// </summary>
        Unknown = 255
    }
}