using Infrastructure.Dkron.Common.Enums.Legacy;

namespace Application.Dkron;

public static class DkronTypeConverter
{
    public static DayOfWeek ToDkronValue(this JobWeeklyFrequencyIntervalTypes type)
    {
        switch (type)
        {
            case JobWeeklyFrequencyIntervalTypes.Sunday: return DayOfWeek.Sunday;
            case JobWeeklyFrequencyIntervalTypes.Monday: return DayOfWeek.Monday;
            case JobWeeklyFrequencyIntervalTypes.Tuesday: return DayOfWeek.Tuesday;
            case JobWeeklyFrequencyIntervalTypes.Wednesday: return DayOfWeek.Wednesday;
            case JobWeeklyFrequencyIntervalTypes.Thursday: return DayOfWeek.Thursday;
            case JobWeeklyFrequencyIntervalTypes.Friday: return DayOfWeek.Friday;
            case JobWeeklyFrequencyIntervalTypes.Saturday: return DayOfWeek.Saturday;
            case JobWeeklyFrequencyIntervalTypes.Unknown:
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}