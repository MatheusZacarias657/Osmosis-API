using Repository.Entities.Patient;
using Repository.Entities.Professional;
using Repository.Interfaces.Base;
using Repository.Interfaces.Patient;

namespace Repository.Repositories.Patient
{
    public class PatientRepository : IPatientRepository, IPatientValidRepository
    {
        private readonly IBaseRepository<PatientEntity> baseRepository;

        public PatientRepository(IBaseRepository<PatientEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public void Add(PatientEntity entity)
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

        public bool CheckIfPatientExist(int id)
        {
            try
            {
                return baseRepository.FindById(id) != null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PatientEntity FindById(int id)
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

        public List<PatientEntity> FindByCustomFilter(Dictionary<string, string>? filters)
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

        public PatientEntity FindByDocument(string document)
        {
            try
            {
                return (from patient in baseRepository.datacontext.Patient
                        where
                        patient.document.Equals(document)
                        && patient.active == true
                        select patient
                       ).ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PatientEntity> GetAllPatients()
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

        public void Update(PatientEntity entity)
        {
            try
            {
                baseRepository.Update(entity);
                baseRepository.Commit();
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
                baseRepository.Delete(id);
                baseRepository.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
