using Repository.Entities.Customer;

namespace Repository.Interfaces.Customer
{
    public interface ICustomerRoleRepository
    {
        List<string> GetAvailableRoles(int roleId);
    }
}