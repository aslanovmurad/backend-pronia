namespace AP204_Pronia.Models
{
    public class PlantImage
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public bool? IsMain { get; set; }

        public int PlantId { get; set; }

        public Plant Plants { get; set; }

    }
}
