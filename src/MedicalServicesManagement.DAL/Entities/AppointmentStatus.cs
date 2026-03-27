namespace MedicalServicesManagement.DAL.Entities
{
    public enum AppointmentStatus
    {
        Free = 0,
        Taken = 1,
        DoneNoPay = 2,
        Cancelled = 3,
        DonePaid = 4,
    }
}
