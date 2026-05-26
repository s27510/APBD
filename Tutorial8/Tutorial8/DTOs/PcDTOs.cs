namespace Tutorial8.DTOs;

public class PcDTOs
{
    public class PcResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Weight { get; set; }
        public int Warranty { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Stock { get; set; }
    }

    public class CreatePcDto
    {
        public string Name { get; set; } = null!;
        public double Weight { get; set; }
        public int Warranty { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Stock { get; set; }
    }

    public class UpdatePcDto
    {
        public string Name { get; set; } = null!;
        public double Weight { get; set; }
        public int Warranty { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Stock { get; set; }
    }
}