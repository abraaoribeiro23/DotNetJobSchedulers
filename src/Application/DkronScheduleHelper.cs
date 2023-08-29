using Infrastructure.Dkron.Common.Enums.Legacy;

namespace Application;

public static class DkronScheduleHelper
{
    public static string GetSchedule(SqlServerJobFrequencyTypes freqType, int freqInterval, DateTime startDate, DateTime activeStartTime,
        DateTime? activeEndTime, SqlServerJobSubDayFrequencyTypes? subDayIntervalType, int? subDayInterval,
        int? freqRelativeInterval, int? freqRecurrenceFactor)
    {
        //Predefined schedules
        //Entry                  | Description                                | Equivalent To
        //-----                  | -----------                                | -------------
        //@yearly (or @annually) | Run once a year, midnight, Jan. 1st        | 0 0 0 1 1 *
        //@monthly               | Run once a month, midnight, first of month | 0 0 0 1 * *
        //@weekly                | Run once a week, midnight on Sunday        | 0 0 0 * * 0
        //@daily (or @midnight)  | Run once a day, midnight                   | 0 0 0 * * *
        //@hourly                | Run once an hour, beginning of hour        | 0 0 * * * *
        //@minutely              | Run once a minute, beginning of minute     | 0 * * * * *
        //@manually              | Never runs                                 | N/A

        string result;
        switch (freqType)
        {
            case SqlServerJobFrequencyTypes.Once:
                result = $"@at {startDate:s}Z"; // 2000-01-30T15:04:05Z
                break;
            case SqlServerJobFrequencyTypes.Daily:
                result = GetDaily(freqInterval,subDayIntervalType,subDayInterval);
                break;
            case SqlServerJobFrequencyTypes.Weekly:
            case SqlServerJobFrequencyTypes.Monthly:
            case SqlServerJobFrequencyTypes.MonthlyRelative:
            case SqlServerJobFrequencyTypes.Unknown:
            default:
                throw new ArgumentOutOfRangeException(nameof(freqType), freqType, null);
        }

        return result;
    }

    //Entry                  | Description                                | Equivalent To
    //-----                  | -----------                                | -------------
    //@daily (or @midnight)  | Run once a day, midnight                   | 0 0 0 * * *
    private static string GetDaily(int freqInterval, SqlServerJobSubDayFrequencyTypes? subDayIntervalType, int? subDayInterval)
    {
        string result;
        switch (subDayIntervalType)
        {
            case SqlServerJobSubDayFrequencyTypes.SpecifiedTime:
                result = "0 0 0 * * *";
                break;
            case SqlServerJobSubDayFrequencyTypes.Seconds:
            case SqlServerJobSubDayFrequencyTypes.Minutes:
            case SqlServerJobSubDayFrequencyTypes.Hours:
            case SqlServerJobSubDayFrequencyTypes.Unknown:
            default:
                throw new ArgumentOutOfRangeException(nameof(subDayIntervalType), subDayIntervalType, null);
        }

        return result;
    }

    private static string GetWeekly(int freqRecurrenceFactor)
    {
        throw new NotImplementedException();
    }
    private static string GetMonthly(int freqRecurrenceFactor)
    {
        throw new NotImplementedException();
    }
    private static string GetMonthlyRelative(int freqRelativeInterval, int freqRecurrenceFactor)
    {
        throw new NotImplementedException();
    }
}