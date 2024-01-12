using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Appointments
{
    public class AppointmentUpdate
    {
        [Required] public int id { get; set; }
        [Required] public int id_status { get; set; }
        public AppointmentRegister appointment { get; set; } = null;
    }
}
