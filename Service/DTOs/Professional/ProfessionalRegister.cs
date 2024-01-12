using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Professional
{
    public class ProfessionalRegister
    {
        [Required] public string name { get; set; }
        [Required] public string specialty { get; set; }
        [Required] public TimeSpan entryTime { get; set; }
        [Required] public TimeSpan departureTime { get; set; }
        [Required] public TimeSpan servicePeriod { get; set; }
        [Required] public string license { get; set; }
    }
}
