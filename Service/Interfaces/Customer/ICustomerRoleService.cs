namespace Service.Interfaces.Customer
{
    public interface ICustomerRoleService
    {
        List<string> GetAvailableRoles(int roleId);
    }
}