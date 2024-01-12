using Repository.Entities.Customer;
using Repository.Interfaces.Base;
using Repository.Interfaces.Customer;

namespace Repository.Repositories.Customer
{
    public class CustomerRoleRepository : ICustomerRoleRepository
    {
        private readonly IBaseRepository<CustomerRoleEntity> baseRepository;

        public CustomerRoleRepository(IBaseRepository<CustomerRoleEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public List<string> GetAvailableRoles(int roleId)
        {
            try
            {
                List<string>? foundRoles = (from role in baseRepository.datacontext.CustomerRole
                                            where role.id >= roleId
                                            select role.name
                                           ).ToList();

                return foundRoles;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
