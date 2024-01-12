using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Appointments
{
    public class AppointmentRegister
    {
        [Required] public DateTime appointmentTime { get; set; }
        [Required] public int id_professional { get; set; }
        [Required] public int id_status { get; set; }
        [Required] public int id_patient { get; set; }
    }
}
