using Repository.Entities.Appointment;
using System.Runtime.InteropServices;

namespace Repository.Interfaces.Appointment
{
    public interface IAppointmentRepository
    {
        void Add(AppointmentEntity entity);
        void ChangeStatus(int appointmentId, int newApppointmentStatus);
        List<AppointmentEntity> FindByCustomFilter(Dictionary<string, string>? filters);
        AppointmentEntity FindById(int id);
        List<AppointmentEntity> GetAllAppointments();
        List<AppointmentEntity> FindAppointmentsByProfessionalId(int professionalId, DateTime dateStart, DateTime dateEnd, [Optional] int status);
    }
}