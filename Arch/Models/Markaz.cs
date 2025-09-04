using Arch.Models;
using System.ComponentModel.DataAnnotations;

namespace AmlakState.Models
{
    public class Markaz
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }

        public ICollection<Madina_Maglas> MadinaMaglas { get; set; } = new List<Madina_Maglas>();

        public ICollection<AgriculturalHolding> AgriculturalHoldings { get; set; } = new List<AgriculturalHolding>();
    }
}
