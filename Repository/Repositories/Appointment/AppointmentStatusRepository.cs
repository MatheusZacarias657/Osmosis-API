using Repository.Entities.Appointment;
using Repository.Interfaces.Appointment;
using Repository.Interfaces.Base;

namespace Repository.Repositories.Appointment
{
    public class AppointmentStatusRepository : IAppointmentStatusRepository
    {
        private readonly IBaseRepository<AppointmentStatusEntity> baseRepository;

        public AppointmentStatusRepository(IBaseRepository<AppointmentStatusEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public AppointmentStatusEntity FindById(int statusId)
        {
            try
            {
                return baseRepository.FindById(statusId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
