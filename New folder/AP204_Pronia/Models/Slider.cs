using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AP204_Pronia.Models
{
    public class Slider
    {
        public int Id { get; set; }

        public string Image { get; set; }
        [StringLength(maximumLength:40)]
        public string Title { get; set; }
        [StringLength(maximumLength: 40)]
        public string Subtitle { get; set; }
        [Range(0,100)]
        public byte Discount { get; set; }
        public string DiscoverUrl { get; set; }
        [Range(0, 10)]
        public byte Order { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
