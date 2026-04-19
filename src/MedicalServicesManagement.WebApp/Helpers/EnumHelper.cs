using MedicalServicesManagement.WebApp.Enums;
using System.Collections.Generic;

namespace MedicalServicesManagement.WebApp.Helpers
{
    internal static class EnumHelper
    {
        public static readonly Dictionary<AppointmentStatus, string> Enums = new ()
        {
            [AppointmentStatus.Free] = "Свободный",
            [AppointmentStatus.Taken] = "Забронированный",
            [AppointmentStatus.DoneNoPay] = "Завершённый, не оплаченный",
            [AppointmentStatus.DonePaid] = "Завершённый, оплаченный",
        };

        public static string Translate(AppointmentStatus status)
        {
            return Enums.GetValueOrDefault(status);
        }
    }
}
