using Repository.Entities.Meeting;

namespace Repository.Interfaces.Appointment
{
    public interface IDailyAppointmentRepository
    {
        List<DailyAppointmentEntity> FindDailyAppointmentByProfessionalId(int professionalId);
    }
}