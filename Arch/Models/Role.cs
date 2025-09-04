using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arch.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }


        public ICollection<User> User { get; set; } = new List<User>();
    }
}
