namespace MedicalServicesManagement.WebApp
{
    public static class Constants
    {
        public const string JwtCookiesKey = "jwt_key";

        public const string GuestRole = "Гость";
        public const string AdminRole = "Администратор";
        public const string PatientRole = "Пациент";
        public const string MedicRole = "Врач";
        public const string ReceptionistRole = "Регистратор";

        public const int FreeAppointmentsPeriodDays = 21;
    }
}
