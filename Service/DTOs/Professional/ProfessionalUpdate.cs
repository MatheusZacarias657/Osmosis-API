using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Professional
{
    public class ProfessionalUpdate
    {
        [Required] public int id { get; set; }
        public string? name { get; set; }
        public string? specialty { get; set; }
        public TimeSpan? entryTime { get; set; }
        public TimeSpan? departureTime { get; set; }
        public TimeSpan? servicePeriod { get; set; }
        [Required] public string license { get; set; }
    }
}
