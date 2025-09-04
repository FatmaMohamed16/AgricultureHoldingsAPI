namespace AmlakState.DTO
{
    public class StatePropertyDTO
    {
        public int Id { get; set; }
        public string? ShaykhaName { get; set; }
        public string? TransgressorName { get; set; }
        public string? NationalId { get; set; }
        public string? PhoneNumber { get; set; }
        public int? BuildingsCount { get; set; }
        public string? Description { get; set; }

        public int MarkazId { get; set; }
        public string? MarkazName { get; set; }

        public string? Address { get; set; }

        public string? NationalIdImagePath { get; set; }


        public String? RequestNumber { get; set; }

        public int? Faddan { get; set; }
        public int? Qirat { get; set; }
        public int? Sahm { get; set; }


        public double? AreaInSquareMeters { get; set; }



        public List<CoordinateDTO> Coordinates { get; set; }

    }
}
