using Repository.Entities.Patient;
using Repository.Interfaces.Patient;
using Service.DTOs.Patient;
using Service.Interfaces.Patient;
using Service.Utils.Tools;

namespace Service.Services.Patient
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository repository;

        public PatientService(IPatientRepository repository)
        {
            this.repository = repository;
        }

        public bool Add(PatientRegister patient)
        {
            try
            {
                if (repository.FindByDocument(patient.document) != null)
                    return false;

                PatientEntity entity = new PatientEntity();
                ObjectTools.CopyProperties(entity, patient, true);
                repository.Add(entity);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PatientUpdate> SearchPatients(Dictionary<string, string>? filters)
        {
            try
            {
                List<PatientEntity> entities = new List<PatientEntity>();
                if (filters.Count == 0 || filters == null)
                    entities = repository.GetAllPatients();
                else
                    entities = repository.FindByCustomFilter(filters);

                List<PatientUpdate> patients = new List<PatientUpdate>();

                foreach (PatientEntity entity in entities)
                {
                    PatientUpdate patient = new PatientUpdate();
                    ObjectTools.CopyProperties(patient, entity, false);
                    patients.Add(patient);
                }

                return patients;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(PatientUpdate patient)
        {
            try
            {
                PatientEntity entity = repository.FindById(patient.id);

                if (entity == null)
                    throw new Exception("This professional doesn't exist");

                ObjectTools.CopyProperties(entity, patient, true);
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
                repository.Delete(id);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
