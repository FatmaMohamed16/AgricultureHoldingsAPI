using Arch.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmlakState.Models
{
    public class AgriculturalHolding
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NationalId { get; set; }


        public string? PhoneNumber { get; set; }

        public int? BuildingsCount { get; set; }

        public string? HyazaNumber { get; set; }


        public string? NorthernBorder { get; set; }

        public string? SouthernBorder { get; set; }

        public string? EasternBorder { get; set; }

        public string? WesternBorder { get; set; }


        public string? Description { get; set; }


        public int MarkazId { get; set; } 
        public Markaz? Markaz { get; set; }

        public string? Address { get; set; }

        public string? Notes  { get; set; }

        public string? DataResourses { get; set; }


        public double? ActualArea { get; set; }

        public double? RegistedArea { get; set; }



        public string? Houd 
            { get; set; }


        public string? Association { get; set; }

        //public int UserId { get; set; }
        //public User? User { get; set; }


        public ICollection<PropertyCoordinate> PropertyCoordinates { get; set; } = new List<PropertyCoordinate>();


        public ICollection<Attachments> Attachments { get; set; } = new List<Attachments>();


        public int? SourceOfOwnershipId { get; set; }
        [ForeignKey("SourceOfOwnershipId")]
        public SourceOfOwnership? SourceOfOwnership { get; set; }

    }
}
