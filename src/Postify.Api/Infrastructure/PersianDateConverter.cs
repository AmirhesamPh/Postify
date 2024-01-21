using Postify.Abstractions.Infrastructure;
using System.Globalization;

namespace Postify.Services;

public class PersianDateConverter : IDateConverter
{
    private readonly PersianCalendar _persianCalendar;

    public PersianDateConverter(PersianCalendar persianCalendar)
    {
        _persianCalendar = persianCalendar;
    }

    public string ToPersianDateTime(DateTime dateTime)
    {
        var year = _persianCalendar
            .GetYear(dateTime)
            .ToString("0000");

        var month = _persianCalendar
            .GetMonth(dateTime)
            .ToString("00");

        var day = _persianCalendar
            .GetDayOfMonth(dateTime)
            .ToString("00");

        return $"{year}/{month}/{day}-{dateTime.Hour}:{dateTime.Minute}";
    }
}
