using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP204_Pronia.ViewModels
{
    public class BasketVM
    {
        public List<BasketItemVM> BasketItemVMs { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal ToyalPrice { get; set; }

        public int Count { get; set; }

        public BasketVM()
        {
            BasketItemVMs = new List<BasketItemVM>();
        }
    }
}
