using Repository.Entities.Customer;
using Repository.Interfaces.Customer;
using Service.DTOs.Customer;
using Service.Interfaces.Customer;
using Service.Utils.Enums;
using Service.Utils.Tools;

namespace Service.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository repository;

        public CustomerService(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        public CustomerRegisterStates Add(CustomerRegister customer)
        {
            try
            {
                if (customer.id_role == 1)
                    throw new Exception("Masters Cannot be created by Rest API");

                CustomerRegisterStates customerState = VerifyCustomerIsUnique(customer.login, customer.email);

                if (customerState != CustomerRegisterStates.NoExist)
                    return customerState;

                CustomerEntity customerEntity = new CustomerEntity();
                ObjectTools.CopyProperties(customerEntity, customer, true);
                customerEntity.password = OAuthTools.EncryptedPassword(customer.password, customer.login);

                repository.Add(customerEntity);

                return CustomerRegisterStates.Created;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private CustomerRegisterStates VerifyCustomerIsUnique(string login, string email)
        {
            try
            {
                CustomerEntity customer = repository.FindCustomerByLoginOrEmail(login, email);

                if (customer == null)
                    return CustomerRegisterStates.NoExist;

                if (customer.email.Equals(email))
                    return CustomerRegisterStates.Email;

                if (customer.login.Equals(login))
                    return CustomerRegisterStates.Login;

                return CustomerRegisterStates.NoExist;
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
                return repository.FindCustomerByLoginOrEmail(login, email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CustomerSearch> SearchCustomers(Dictionary<string, string>? filters)
        {
            try
            {
                List<CustomerEntity> entities = new List<CustomerEntity>();
                if (filters.Count == 0 || filters == null)
                    entities = repository.GetAllCustomers();
                else
                    entities = repository.FindByCustomFilter(filters);

                List<CustomerSearch> customers = new List<CustomerSearch>();

                foreach (CustomerEntity entity in entities)
                {
                    CustomerSearch customer = new CustomerSearch();
                    ObjectTools.CopyProperties(customer, entity, false);
                    customers.Add(customer);
                }

                return customers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int FindRoleByCustomerId(int id)
        {
            try
            {
                CustomerEntity entity = repository.FindById(id);

                if (entity == null)
                    throw new Exception("The customer doesn't exists");

                return entity.id_role;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(CustomerUpdate customer)
        {
            try
            {
                CustomerEntity customerEntity = repository.FindById(customer.id);

                if (customerEntity == null)
                    throw new Exception("This customer doesn't exist");

                ObjectTools.CopyProperties(customerEntity, customer, true);
                customerEntity.password = OAuthTools.EncryptedPassword(customer.password, customer.login);
                repository.Update(customerEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdatePassword(int customerId, string newPassword)
        {
            try
            {
                CustomerEntity customerEntity = repository.FindById(customerId);

                if(customerEntity == null)
                    throw new Exception("This customer doesn't exist");

                customerEntity.password = OAuthTools.EncryptedPassword(newPassword, customerEntity.login);
                repository.Update(customerEntity);
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
                repository.Delete(id);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
