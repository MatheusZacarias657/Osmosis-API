using Repository.Entities.Professional;
using Repository.Interfaces.Appointment;
using Repository.Interfaces.Professional;
using Service.DTOs.Professional;
using Service.Interfaces.Professional;
using Service.Utils.Enums;
using Service.Utils.Tools;

namespace Service.Services.Professional
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly IProfessionalRepository repository;
        private readonly IAppointmentProfessionalRepository appointmentRepository;

        public ProfessionalService(IProfessionalRepository repository, IAppointmentProfessionalRepository appointmentRepository)
        {
            this.repository = repository;
            this.appointmentRepository = appointmentRepository;
        }

        public bool Add(ProfessionalRegister professional)
        {
            try
            {
                if (professional.entryTime > professional.departureTime)
                    throw new Exception("Invalid office hours");

                if (repository.FindBylicense(professional.license) != null)
                    return false;

                ProfessionalEntity entity = new ProfessionalEntity();
                ObjectTools.CopyProperties(entity, professional, true);
                repository.Add(entity);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ProfessionalUpdate> SearchProfessionals(Dictionary<string, string>? filters)
        {
            try
            {
                List<ProfessionalEntity> entities = new List<ProfessionalEntity>();
                if (filters.Count == 0 || filters == null)
                    entities = repository.GetAllProfessionals();
                else
                    entities = repository.FindByCustomFilter(filters);

                List<ProfessionalUpdate> professionals = new List<ProfessionalUpdate>();

                foreach (ProfessionalEntity entity in entities)
                {
                    ProfessionalUpdate professional = new ProfessionalUpdate();
                    ObjectTools.CopyProperties(professional, entity, false);
                    professionals.Add(professional);
                }

                return professionals;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(ProfessionalUpdate professional)
        {
            try
            {
                ProfessionalEntity entity = repository.FindById(professional.id);

                if (entity == null)
                    throw new Exception("This professional doesn't exist");

                ObjectTools.CopyProperties(entity, professional, true);

                if (entity.entryTime > entity.departureTime)
                    throw new Exception("Invalid office hours");

                repository.Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                appointmentRepository.ChangeStatusByProfessionalId(id, (int) AppointmentStatus.Canceled);
                repository.Delete(id);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
