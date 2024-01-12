using Repository.Entities.Patient;

namespace Repository.Interfaces.Patient
{
    public interface IPatientRepository
    {
        void Add(PatientEntity entity);
        bool CheckIfPatientExist(int id);
        PatientEntity FindByDocument(string document);
        void Delete(int id);
        List<PatientEntity> FindByCustomFilter(Dictionary<string, string>? filters);
        PatientEntity FindById(int id);
        List<PatientEntity> GetAllPatients();
        void Update(PatientEntity entity);
    }
}