using System.ComponentModel.DataAnnotations;

namespace Arch.DTO
{
    public class AgriculturalHoldingUpdateDto
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
        public int? MarkazId { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public string? DataResourses { get; set; }
        public int? Faddan { get; set; }
        public int? Qirat { get; set; }
        public int? Sahm { get; set; }
        public double? ActualAreaInSquareMeters { get; set; }
        public double? RegistedArea { get; set; }
        public int? MadinaMaglasId { get; set; }
        public string? Houd { get; set; }
        public string? Association { get; set; }
        public int? SourceOfOwnershipId { get; set; }

        public List<IFormFile>? NewAttachments { get; set; } = new List<IFormFile>();


        public List<string>? ExistingAttachments { get; set; } = new List<string>();


        public List<PropertyCoordinateDto>? PropertyCoordinates { get; set; }

    }
}
