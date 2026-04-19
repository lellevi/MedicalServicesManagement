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
    }
}
