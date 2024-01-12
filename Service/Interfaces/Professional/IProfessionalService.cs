using Service.DTOs.Professional;

namespace Service.Interfaces.Professional
{
    public interface IProfessionalService
    {
        bool Add(ProfessionalRegister professional);
        void Delete(int id);
        List<ProfessionalUpdate> SearchProfessionals(Dictionary<string, string>? filters);
        void Update(ProfessionalUpdate professional);
    }
}