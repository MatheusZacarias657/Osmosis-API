using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Patient
{
    public class PatientUpdate
    {
        [Required] public int id { get; set; }
        public string document { get; set; }
        public string name { get; set; }
        public DateOnly birthday { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
    }
}
