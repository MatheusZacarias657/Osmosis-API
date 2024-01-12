namespace Repository.Interfaces.Appointment
{
    public interface IAppointmentProfessionalRepository
    {
        void ChangeStatusByProfessionalId(int professionalId, int newApppointmentStatus);
    }
}