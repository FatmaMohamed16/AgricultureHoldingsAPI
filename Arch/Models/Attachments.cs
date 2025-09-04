using AmlakState.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arch.Models
{
    public class Attachments
    {
        [Key]
        public int ID { get; set; }

         public string? Attachment { get; set; }


        public int? AgriculturalHoldingId { get; set; }
        [ForeignKey("AgriculturalHoldingId")]
        public AgriculturalHolding? AgriculturalHolding { get; set; }

    }
}
