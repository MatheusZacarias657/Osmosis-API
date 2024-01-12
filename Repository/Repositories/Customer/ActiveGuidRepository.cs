using Repository.Entities.Customer;
using Repository.Interfaces.Base;
using Repository.Interfaces.Customer;

namespace Repository.Repositories.Customer
{
    public class ActiveGuidRepository : IActiveGuidRepository
    {
        private readonly IBaseRepository<ActiveGuidEntity> baseRepository;

        public ActiveGuidRepository(IBaseRepository<ActiveGuidEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public void Add(ActiveGuidEntity entity)
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

        public ActiveGuidEntity FindSessionByGuid(string guid, DateTime now)
        {
            try
            {
                ActiveGuidEntity session = baseRepository.datacontext.ActiveGuid.Where(x => x.guid.Equals(guid) && now < x.expirationDate.Value).FirstOrDefault();

                if (session == null)
                    throw new Exception("user not logged");

                return session;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveBrowserSessionByUser(int id, string browser, DateTime now)
        {
            try
            {
                List<ActiveGuidEntity> sessions = baseRepository.datacontext.ActiveGuid.Where(x => x.id_customer == id && x.browser.Equals(browser)).ToList();
                sessions.ForEach(x => x.expirationDate = now);
                baseRepository.datacontext.ActiveGuid.UpdateRange(sessions);
                baseRepository.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveAllSessionsByUser(int id, DateTime now)
        {
            try
            {
                List<ActiveGuidEntity> sessions = baseRepository.datacontext.ActiveGuid.Where(x => x.id_customer == id).ToList();
                sessions.ForEach(x => x.expirationDate = now);
                baseRepository.datacontext.ActiveGuid.UpdateRange(sessions);
                baseRepository.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
