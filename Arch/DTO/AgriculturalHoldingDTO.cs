namespace Arch.DTO
{
    public class AgriculturalHoldingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string? PhoneNumber { get; set; }
        public int BuildingsCount { get; set; }
        public string? HyazaNumber { get; set; }
        public string? NorthernBorder { get; set; }
        public string? SouthernBorder { get; set; }
        public string? EasternBorder { get; set; }
        public string? WesternBorder { get; set; }
        public string? Description { get; set; }
        public int MarkazId { get; set; }
        public string? MarkazName { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public string? DataResourses { get; set; }
        public int Faddan { get; set; }
        public int Qirat { get; set; }
        public int Sahm { get; set; }
        public double ActualAreaInSquareMeters { get; set; }
        public double RegistedArea { get; set; }
        public string? MadinaMaglasName { get; set; }
        public string? Houd { get; set; }
        public string? Association { get; set; }
        public string? SourceOfOwnershipName { get; set; }
        public List<PropertyCoordinateDto> Coordinates { get; set; }
        public List<string> Attachments { get; set; }
    }
}
