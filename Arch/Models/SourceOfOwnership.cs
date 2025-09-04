using AmlakState.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arch.Models
{
    public class SourceOfOwnership
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }


     


        public ICollection<AgriculturalHolding> AgriculturalHoldings { get; set; } = new List<AgriculturalHolding>();
    }
}
