using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Customer
{
    public class CustomerSearch
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? login { get; set; }
        public string? email { get; set; }
        public int? id_role { get; set; }
    }
}
