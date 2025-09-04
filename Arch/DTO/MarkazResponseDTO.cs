namespace AmlakState.DTO
{
    public class MarkazResponseDTO
    {
        public bool IsAdmin { get; set; }
        public int? UserMarkazId { get; set; } 
        public List<MarkazDTO> Markazes { get; set; }
    }
}
