using Repository.Entities.Professional;

namespace Repository.Interfaces.Professional
{
    public interface IProfessionalRepository
    {
        void Add(ProfessionalEntity entity);
        void Delete(int id);
        List<ProfessionalEntity> FindByCustomFilter(Dictionary<string, string>? filters);
        ProfessionalEntity FindBylicense(string license);
        ProfessionalEntity FindById(int id);
        void Update(ProfessionalEntity entity);
        List<ProfessionalEntity> GetAllProfessionals();
        bool CheckIfProfesionalExist(int id);
    }
}