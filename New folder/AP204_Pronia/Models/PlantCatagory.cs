namespace AP204_Pronia.Models
{
    public class PlantCatagory
    {
        public int Id { get; set; }

        public int CatagoryId { get; set; }

        public Catagory Catagory { get; set; }

        public int PlantId { get; set; }

        public Plant Plant { get; set; }
    }
}
