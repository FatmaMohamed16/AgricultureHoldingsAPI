using System.ComponentModel.DataAnnotations;

namespace AmlakState.Models
{
    public class PropertyCoordinate
    {
        [Key]
        public int ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public int AgriculturalHoldingId { get; set; }
        public AgriculturalHolding? AgriculturalHolding { get; set; }
    }
}
