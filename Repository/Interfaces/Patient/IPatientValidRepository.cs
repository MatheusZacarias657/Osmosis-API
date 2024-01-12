namespace Repository.Interfaces.Patient
{
    public interface IPatientValidRepository
    {
        bool CheckIfPatientExist(int id);
    }
}