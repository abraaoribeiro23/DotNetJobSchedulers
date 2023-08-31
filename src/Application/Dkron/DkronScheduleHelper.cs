using Infrastructure.Dkron.Common.Enums.Legacy;

namespace Application.Dkron;

public static class DkronScheduleHelper
{
    public static string GetSchedule(SqlServerJobFrequencyTypes freqType, int freqInterval, DateTime startDate, DateTime activeStartTime,
        DateTime? activeEndTime, SqlServerJobSubDayFrequencyTypes subDayIntervalType, int? subDayInterval, int? freqRelativeInterval,
        int? freqRecurrenceFactor)
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
        var subDaySchedule = GetSubDayInterval(subDayIntervalType, subDayInterval, activeStartTime, activeEndTime);
        switch (freqType)
        {
            case SqlServerJobFrequencyTypes.Once:
                result = $"@at {startDate:s}Z"; // 2000-01-30T15:04:05Z
                break;
            case SqlServerJobFrequencyTypes.Daily:
                result = $"{subDaySchedule} {GetDaily(freqInterval)}";
                break;
            case SqlServerJobFrequencyTypes.Weekly:
                result = $"{subDaySchedule} {GetWeekly(freqInterval)}";
                break;
            case SqlServerJobFrequencyTypes.Monthly:
                result = $"{subDaySchedule} {GetMonthly(freqInterval)}";
                break;
            case SqlServerJobFrequencyTypes.MonthlyRelative:
            case SqlServerJobFrequencyTypes.Unknown:
            default:
                throw new ArgumentOutOfRangeException(nameof(freqType), freqType, null);
        }

        return result;
    }

    public static string GetSubDayInterval(SqlServerJobSubDayFrequencyTypes subDayIntervalType, int? subDayInterval, DateTime activeStartTime,
        DateTime? activeEndTime)
    {
        var hourInterval = activeStartTime.Hour.ToString();
        var minuteInterval = activeStartTime.Minute.ToString();
        var secondInterval = activeStartTime.Second.ToString();

        if (activeEndTime.HasValue)
        {
            hourInterval += $"-{activeEndTime.Value.Hour}";
            minuteInterval += $"-{activeEndTime.Value.Minute}";
            secondInterval += $"-{activeEndTime.Value.Second}";
        }

        var subDayIntervalSlash = string.Empty;
        if (subDayInterval >= 1)
            subDayIntervalSlash = $"/{subDayInterval}";

        string result;
        switch (subDayIntervalType)
        {
            case SqlServerJobSubDayFrequencyTypes.SpecifiedTime:
                result = $"{secondInterval} {minuteInterval} {hourInterval}";
                break;
            case SqlServerJobSubDayFrequencyTypes.Seconds:
                result = $"{secondInterval}{subDayIntervalSlash} {minuteInterval} {hourInterval}";
                break;
            case SqlServerJobSubDayFrequencyTypes.Minutes:
                result = $"{secondInterval} {minuteInterval}{subDayIntervalSlash} {hourInterval}";
                break;
            case SqlServerJobSubDayFrequencyTypes.Hours:
                result = $"{secondInterval} {minuteInterval} {hourInterval}{subDayIntervalSlash}";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(subDayIntervalType), subDayIntervalType, null);
        }

        return result;
    }

    private static string GetDaily(int freqInterval)
    {
        var freqIntervalSlash = string.Empty;
        if (freqInterval >= 1)
            freqIntervalSlash = $"/{freqInterval}";

        var result = $"*{freqIntervalSlash} * *";
        return result;
    }

    private static string GetMonthly(int freqInterval)
    {
        var freqIntervalSlash = string.Empty;
        if (freqInterval >= 1)
            freqIntervalSlash = $"/{freqInterval}";

        var result = $"* *{freqIntervalSlash} *";
        return result;
    }
    private static string GetMonthlyRelative(int freqRelativeInterval, int freqRecurrenceFactor)
    {
        throw new NotImplementedException();
    }

    private static string GetWeekly(int freqInterval)
    {
        var selectedDays = new List<DayOfWeek>();

        var binaryString = Convert.ToString(freqInterval, 2).PadLeft(7, '0');
        StringHelper.Reverse(ref binaryString);

        var counter = 0;
        foreach (var c in binaryString)
        {
            if (c == '1')
            {
                var weekDay = (JobWeeklyFrequencyIntervalTypes)Enum
                    .ToObject(typeof(JobWeeklyFrequencyIntervalTypes), (int)Math.Pow(2, counter));

                var dkronWeekDay = weekDay.ToDkronValue();
                selectedDays.Add(dkronWeekDay);
            }
            counter++;
        }
        var weekInterval = string.Join(",", selectedDays.Select(s => (int)s));
        var result = $"* * {weekInterval}";
        return result;
    }
}