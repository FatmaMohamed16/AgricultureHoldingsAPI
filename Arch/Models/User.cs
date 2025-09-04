using AmlakState.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Arch.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }  
        
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public int MarkazId { get; set; }
        public Markaz? Markaz { get; set; }



        public int UserRoleId { get; set; }
        [ForeignKey("UserRoleId")]
        public virtual Role? UserRole { get; set; }



    }
}
