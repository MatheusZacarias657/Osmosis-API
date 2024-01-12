using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Customer
{
    public class ActiveGuid
    {
        public string guid { get; set; }
        public int id_customer { get; set; }
        public string browser { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime? expirationDate { get; set; }
    }
}
