using Repository.Entities.Customer;
using Repository.Interfaces.Base;
using Repository.Interfaces.Customer;

namespace Repository.Repositories.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IBaseRepository<CustomerEntity> baseRepository;

        public CustomerRepository(IBaseRepository<CustomerEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public void Add(CustomerEntity entity)
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

        public CustomerEntity FindById(int id)
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

        public List<CustomerEntity> FindByCustomFilter(Dictionary<string, string>? filters)
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

        public CustomerEntity FindCustomerByLoginOrEmail(string login, string email)
        {
            try
            {
                CustomerEntity? foundCustomer = (from customer in baseRepository.datacontext.Customer
                                       where 
                                       (customer.login.Equals(login) || customer.email.Equals(email)
                                       || customer.login.Equals(email) || customer.email.Equals(login)) 
                                       && customer.active == true
                                       select customer
                                       ).ToList().FirstOrDefault();

                return foundCustomer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomerEntity> GetAllCustomers()
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

        public void Update(CustomerEntity entity)
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
