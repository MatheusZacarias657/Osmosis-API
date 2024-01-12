using Repository.Entities.Customer;
using Service.DTOs.Customer;
using Service.Utils.Enums;

namespace Service.Interfaces.Customer
{
    public interface ICustomerService
    {
        CustomerRegisterStates Add(CustomerRegister customer);
        void Delete(int id);
        int FindRoleByCustomerId(int id);
        List<CustomerSearch> SearchCustomers(Dictionary<string, string>? filters);
        void Update(CustomerUpdate customer);
        CustomerEntity FindCustomerByLoginOrEmail(string login, string email);
        public void UpdatePassword(int customerId, string newPassword);
    }
}