using System.ComponentModel.DataAnnotations;

namespace Arch.DTO
{
    public class AgriculturalHoldingCreateDto
    {
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
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public string? DataResourses { get; set; }
        public int? Faddan { get; set; }
        public int? Qirat { get; set; }
        public int? Sahm { get; set; }
        public double? ActualAreaInSquareMeters { get; set; }
        public double? RegistedArea { get; set; }
        public string? Houd { get; set; }
        public string? Association { get; set; }
        [Required]
        public int MarkazId { get; set; }

        [Required]
        public int SourceOfOwnershipId { get; set; }



        public int MadinaMaglasId { get; set; }

        public List<PropertyCoordinateDto>? PropertyCoordinates { get; set; }

        public List<IFormFile>? Attachments { get; set; }
    }
}
