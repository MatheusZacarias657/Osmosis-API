using Repository.Entities.Customer;

namespace Repository.Interfaces.Customer
{
    public interface ICustomerRepository
    {
        void Add(CustomerEntity entity);
        void Delete(int id);
        List<CustomerEntity> FindByCustomFilter(Dictionary<string, string>? filters);
        CustomerEntity FindById(int id);
        public CustomerEntity FindCustomerByLoginOrEmail(string login, string email);
        void Update(CustomerEntity entity);
        List<CustomerEntity> GetAllCustomers();
    }
}