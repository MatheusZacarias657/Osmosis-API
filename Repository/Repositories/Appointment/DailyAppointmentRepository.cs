using Repository.Entities.Meeting;
using Repository.Interfaces.Appointment;
using Repository.Interfaces.Base;

namespace Repository.Repositories.Appointment
{
    public class DailyAppointmentRepository : IDailyAppointmentRepository
    {
        private readonly IBaseRepository<DailyAppointmentEntity> baseRepository;

        public DailyAppointmentRepository(IBaseRepository<DailyAppointmentEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public List<DailyAppointmentEntity> FindDailyAppointmentByProfessionalId(int professionalId)
        {
            try
            {
                Dictionary<string, string> filters = new Dictionary<string, string>()
                {
                    {"id_professional", professionalId.ToString()}
                };

                return baseRepository.FindByCustomFilter(filters);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
