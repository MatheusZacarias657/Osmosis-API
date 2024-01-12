using System.ComponentModel.DataAnnotations;

namespace Repository.Entities.Professional
{
    public class ProfessionalEntity
    {
        [Key] public int id { get; set; }
        public string? name { get; set; }
        public string? specialty { get; set; }
        public TimeSpan? entryTime { get; set; }
        public TimeSpan? departureTime { get; set; }
        public TimeSpan? servicePeriod { get; set; }
        public string license { get; set; }
        public bool active { get; set; } = true;
    }
}
