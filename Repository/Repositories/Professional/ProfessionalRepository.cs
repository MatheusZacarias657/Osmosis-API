using Repository.Entities.Customer;
using Repository.Entities.Professional;
using Repository.Interfaces.Base;
using Repository.Interfaces.Professional;

namespace Repository.Repositories.Professional
{
    public class ProfessionalRepository : IProfessionalRepository, IProfessionalValidRepository
    {
        private readonly IBaseRepository<ProfessionalEntity> baseRepository;

        public ProfessionalRepository(IBaseRepository<ProfessionalEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public void Add(ProfessionalEntity entity)
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

        public bool CheckIfProfesionalExist(int id)
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

        public ProfessionalEntity FindBylicense(string license)
        {
            try
            {
                return (from professional in baseRepository.datacontext.Professional
                        where
                        professional.license.Equals(license)
                        && professional.active == true
                        select professional
                                       ).ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProfessionalEntity FindById(int id)
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

        public List<ProfessionalEntity> FindByCustomFilter(Dictionary<string, string>? filters)
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

        public List<ProfessionalEntity> GetAllProfessionals()
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

        public void Update(ProfessionalEntity entity)
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
