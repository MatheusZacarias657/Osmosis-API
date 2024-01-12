using Repository.Entities.Customer;
using Repository.Interfaces.Customer;
using Service.DTOs.Customer;
using Service.Interfaces.Customer;
using Service.Utils.Tools;

namespace Service.Services.Customer
{
    public class ActiveGuidService : IActiveGuidService
    {
        private readonly IActiveGuidRepository repository;

        public ActiveGuidService(IActiveGuidRepository repository)
        {
            this.repository = repository;
        }

        public void AddNewGuid(int customerId)
        {
            try
            {
                ActiveGuidEntity activeGuid = new ActiveGuidEntity()
                {
                    id_customer = customerId,
                    creationDate = NormalizeDate.GetCurrentDateTime(),
                    guid = Guid.NewGuid().ToString()
                };

                repository.Add(activeGuid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActiveGuid FindSessionByGuid(string guid)
        {
            try
            {
                ActiveGuidEntity activeGuid = repository.FindSessionByGuid(guid, NormalizeDate.GetCurrentDateTime());
                ActiveGuid activeDTO = new ActiveGuid();
                ObjectTools.CopyProperties(activeDTO, activeGuid, false);

                return activeDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveBrowserSessionByUser(int customerId, string browser)
        {
            try
            {
                repository.RemoveBrowserSessionByUser(customerId, browser, NormalizeDate.GetCurrentDateTime());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveAllSessionsByUser(int customerId)
        {
            try
            {
                repository.RemoveAllSessionsByUser(customerId, NormalizeDate.GetCurrentDateTime());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
