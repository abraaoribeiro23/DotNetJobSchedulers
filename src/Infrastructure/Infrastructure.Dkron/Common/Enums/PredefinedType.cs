namespace Infrastructure.Dkron.Common.Enums;

public enum PredefinedType
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

    Yearly,
    Monthly,
    Weekly,
    Daily,
    Hourly,
    Minutely,
    Manually
}