using Repository.Entities.Customer;

namespace Repository.Interfaces.Customer
{
    public interface IActiveGuidRepository
    {
        void Add(ActiveGuidEntity entity);
        ActiveGuidEntity FindSessionByGuid(string guid, DateTime now);
        void RemoveAllSessionsByUser(int id, DateTime now);
        void RemoveBrowserSessionByUser(int id, string browser, DateTime now);
    }
}