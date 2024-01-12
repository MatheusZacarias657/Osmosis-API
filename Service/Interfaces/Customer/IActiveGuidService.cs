using Service.DTOs.Customer;

namespace Service.Interfaces.Customer
{
    public interface IActiveGuidService
    {
        void AddNewGuid(int customerId);
        ActiveGuid FindSessionByGuid(string guid);
        void RemoveAllSessionsByUser(int customerId);
        void RemoveBrowserSessionByUser(int customerId, string browser);
    }
}