using System;

namespace MedicalServicesManagement.WebApp.Helpers
{
    public static class TimeFormatHelper
    {
        public static string FormatDuration(DateTime start, DateTime end)
        {
            var duration = end - start;

            if (duration.TotalHours >= 1)
            {
                return $"{(int)duration.TotalHours}ч {duration.Minutes:D2}мин";
            }

            return $"{duration.Minutes}мин";
        }

        public static string FormatTimeRange(DateTime start, DateTime end)
        {
            return $"{start:HH:mm}-{end:HH:mm}";
        }

        public static string FormatDays(int days)
        {
            var lastTwoDigits = days % 100;
            var lastDigit = days % 10;

            if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
            {
                return $"{days} дней";
            }

            if (lastDigit == 1)
            {
                return $"{days} день";
            }

            if (lastDigit >= 2 && lastDigit <= 4)
            {
                return $"{days} дня";
            }

            return $"{days} дней";
        }
    }
}
