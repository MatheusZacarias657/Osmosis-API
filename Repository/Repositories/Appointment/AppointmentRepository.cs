using Repository.Entities.Appointment;
using Repository.Interfaces.Appointment;
using Repository.Interfaces.Base;
using System.Runtime.InteropServices;

namespace Repository.Repositories.Appointment
{
    public class AppointmentRepository : IAppointmentRepository, IAppointmentProfessionalRepository
    {
        private readonly IBaseRepository<AppointmentEntity> baseRepository;

        public AppointmentRepository(IBaseRepository<AppointmentEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public void Add(AppointmentEntity entity)
        {
            try
            {
                baseRepository.Create(entity);
                baseRepository.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AppointmentEntity FindById(int id)
        {
            try
            {
                return baseRepository.FindById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AppointmentEntity> FindByCustomFilter(Dictionary<string, string>? filters)
        {
            try
            {
                return baseRepository.FindByCustomFilter(filters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AppointmentEntity> FindAppointmentsByProfessionalId(int professionalId, DateTime dateStart, DateTime dateEnd, [Optional] int status)
        {
            try
            {
                status = (status == default) ? 1 : status;

                return baseRepository.datacontext.Appointment.Where(appointment =>
                                                            appointment.id_professional == professionalId
                                                            && appointment.appointmentTime.Date >= dateStart.Date
                                                            && appointment.appointmentTime.Date <= dateEnd.Date
                                                            && appointment.id_status == status).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AppointmentEntity> GetAllAppointments()
        {
            try
            {
                return baseRepository.GetAllRegisters();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ChangeStatus(int appointmentId, int newApppointmentStatus)
        {
            try
            {
                AppointmentEntity entity = baseRepository.FindById(appointmentId);
                entity.id_status = newApppointmentStatus;
                baseRepository.Update(entity);
                baseRepository.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ChangeStatusByProfessionalId(int professionalId, int newApppointmentStatus)
        {
            try
            {
                List<AppointmentEntity> appointments = baseRepository.datacontext.Appointment.Where(x => x.id_professional == professionalId).ToList();
                appointments.ForEach(x => x.id_status = newApppointmentStatus);
                baseRepository.datacontext.Appointment.UpdateRange(appointments);
                baseRepository.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
