using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.Customer
{
    public class CustomerEntity
    {
        [Key] public int id { get; set; }
        public string? name { get; set; }
        public string? login { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public int id_role { get; set; }
        public bool active { get; set; } = true;
    }
}
