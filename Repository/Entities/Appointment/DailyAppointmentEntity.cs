using System.ComponentModel.DataAnnotations;

namespace Repository.Entities.Meeting
{
    public class DailyAppointmentEntity
    {
        [Key] public int id { get; set; }
        public TimeSpan startTime { get; set; }
        public int id_professional { get; set; }
    }
}
