using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Customer
{
    public class CustomerUpdate
    {
        [Required] public int id { get; set; }
        public string? name { get; set; }
        public string login { get; set; }
        [EmailAddress] public string email { get; set; }
        public string? password { get; set; }
        public int? id_role { get; set; }
    }
}
