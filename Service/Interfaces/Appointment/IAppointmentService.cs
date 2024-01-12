using Service.DTOs.Appointments;
using Service.Utils.Enums;

namespace Service.Interfaces.Appointment
{
    public interface IAppointmentService
    {
        bool Add(AppointmentRegister appointment);
        List<DateTime> FindAppointmentsAvailable(int profissionalId, DateTime dateStart, DateTime dateEnd);
        List<AppointmentRegister> SearchAppointments(Dictionary<string, string>? filters);
        void Update(AppointmentUpdate appointment);
    }
}