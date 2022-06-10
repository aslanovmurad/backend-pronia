using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP204_Pronia.Models
{
    public class Plant
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public string SKUCode { get; set; }
        [Required]

        public string Shipping { get; set; }
        public string Request { get; set; }
        [Required]

        public string Guarantee { get; set; }
        public int? ColorId { get; set; }
        public Color Color { get; set; }
        public int? SizeId { get; set; }
        public Size Size { get; set; }
        public List<PlantImage> PlantImages { get; set; }
        public List<PlantCatagory> PlantCatagories { get; set; }


        [NotMapped]
        public IFormFile MainImage { get; set; }

        [NotMapped]
        public List<IFormFile> AnotherImage { get; set; }

        [NotMapped]
        public IList<int>  ImagesIds { get; set; }
        [NotMapped]
        public IList<int> CatagoriesIds { get; set; }


    }
}
