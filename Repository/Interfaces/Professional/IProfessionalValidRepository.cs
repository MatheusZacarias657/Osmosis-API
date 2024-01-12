using Repository.Entities.Professional;

namespace Repository.Interfaces.Professional
{
    public interface IProfessionalValidRepository
    {
        bool CheckIfProfesionalExist(int id);
        List<ProfessionalEntity> GetAllProfessionals();
        void Update(ProfessionalEntity entity);
    }
}