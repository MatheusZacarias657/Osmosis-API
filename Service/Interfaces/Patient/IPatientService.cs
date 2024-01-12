using Service.DTOs.Patient;

namespace Service.Interfaces.Patient
{
    public interface IPatientService
    {
        bool Add(PatientRegister patient);
        void Delete(int id);
        List<PatientUpdate> SearchPatients(Dictionary<string, string>? filters);
        void Update(PatientUpdate patient);
    }
}