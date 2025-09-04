using AmlakState.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arch.Models
{
    public class Madina_Maglas
    {
       [Key]
        public int ID { get; set; }
        public String Name { get; set; }
        
        public int MarkazId { get; set; }
        [ForeignKey("MarkazId")]
        public Markaz? Markaz { get; set; }


    }
}
