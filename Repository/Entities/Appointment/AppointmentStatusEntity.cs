using System.ComponentModel.DataAnnotations;

namespace Repository.Entities.Appointment
{
    public class AppointmentStatusEntity
    {
        [Key] public int id { get; set; }
        public string name { get; set; }
        public bool createNewAppointment { get; set; }
    }
}
