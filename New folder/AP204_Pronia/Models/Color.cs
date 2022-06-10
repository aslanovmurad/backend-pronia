using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AP204_Pronia.Models
{
    public class Color
    {
        public int Id { get; set; }

        [StringLength(maximumLength:10)]

        public string Name { get; set; }

        public List<Plant> Plants { get; set; }
    }
}
