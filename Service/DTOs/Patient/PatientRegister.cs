using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Patient
{
    public class PatientRegister
    {
        [Required] public string document { get; set; }
        [Required] public string name { get; set; }
        [Required] public DateOnly birthday { get; set; }
        [Required] public string email { get; set; }
        [Required] public string telephone { get; set; }
    }
}
