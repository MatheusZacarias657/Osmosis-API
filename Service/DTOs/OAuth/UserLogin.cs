using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.OAuth
{
    public class UserLogin
    {
        [Required] public string username { get; set; }
        [Required] public string password { get; set; }
        [Required] public string browser { get; set; }
    }
}
