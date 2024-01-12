using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.Patient
{
    public class PatientEntity
    {
        [Key] public int id { get; set; }
        public string document { get; set; }
        public string name { get; set; }
        public DateOnly birthday { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public bool active { get; set; } = true;
    }
}
