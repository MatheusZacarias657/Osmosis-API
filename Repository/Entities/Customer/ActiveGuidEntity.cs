using System.ComponentModel.DataAnnotations;

namespace Repository.Entities.Customer
{
    public class ActiveGuidEntity
    {
        [Key] public int id { get; set; }
        public string guid { get; set; }
        public int id_customer { get; set; }
        public string browser { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime? expirationDate { get; set; }
    }
}
