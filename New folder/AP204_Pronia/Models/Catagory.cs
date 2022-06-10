using System.Collections.Generic;

namespace AP204_Pronia.Models
{
    public class Catagory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PlantCatagory> PlantCatagories { get; set; }
    }
}
