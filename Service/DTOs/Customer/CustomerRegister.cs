using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Customer
{
    public class CustomerRegister
    {
        [Required] public string name { get; set; }
        [Required] public string login { get; set; }
        [Required][EmailAddress] public string email { get; set; }
        [Required] public string password { get; set; }
        [Required] public int id_role { get; set; }
    }
}
