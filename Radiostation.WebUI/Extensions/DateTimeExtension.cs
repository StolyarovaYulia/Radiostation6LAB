using System;

namespace Radiostation.WebUI.Extensions
{
    public static class DateTimeExtension
    {
        public static bool IsEqualDay(this DateTime date, DateTime anotherDate)
        {
            return date.Day == anotherDate.Day && date.Month == anotherDate.Month && date.Year == anotherDate.Year;
        }
    }
}