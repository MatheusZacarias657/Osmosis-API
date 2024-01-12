using System.ComponentModel.DataAnnotations;

namespace Repository.Entities.Appointment
{
    public class AppointmentEntity
    {
        [Key] public int id { get; set; }
        public DateTime appointmentTime { get; set; }
        public int id_professional { get; set; }
        public int id_status { get; set; }
        public int id_patient { get; set; }
    }
}
