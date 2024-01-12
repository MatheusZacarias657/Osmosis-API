using Repository.Interfaces.Customer;
using Service.Interfaces.Customer;

namespace Service.Services.Customer
{
    public class CustomerRoleService : ICustomerRoleService
    {
        private readonly ICustomerRoleRepository repository;

        public CustomerRoleService(ICustomerRoleRepository repository)
        {
            this.repository = repository;
        }

        public List<string> GetAvailableRoles(int roleId)
        {
            try
            {
                return repository.GetAvailableRoles(roleId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
