using Repository.Entities.Appointment;

namespace Repository.Interfaces.Appointment
{
    public interface IAppointmentStatusRepository
    {
        AppointmentStatusEntity FindById(int statusId);
    }
}