using System;

namespace MedicalServicesManagement.WebApp.Helpers
{
    public static class TimeFormatHelper
    {
        public static string FormatDuration(TimeSpan duration)
        {
            if (duration.TotalHours >= 1)
            {
                return $"{(int)duration.TotalHours}ч {(int)duration.Minutes:D2}мин";
            }

            return $"{duration.Minutes}мин";
        }
    }
}
