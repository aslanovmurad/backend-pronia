using AP204_Pronia.DAL;
using AP204_Pronia.Models;
using AP204_Pronia.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP204_Pronia.Services
{
    public class LayoutService
    {
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LayoutService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Setting>> getData()
        {
            List<Setting> setting = await context.settings.ToListAsync();

            return setting;
        }

        public async Task<BasketVM> getbasket()
        {
            string basket = httpContextAccessor.HttpContext.Request.Cookies["basket"];
            BasketVM basketVM = new BasketVM();

            if (!string.IsNullOrEmpty(basket))
            {
                List<BasketCookieItemVM> basketList = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basket);

                //List<Plant> query = await context.Plants.ToListAsync();

                var query = context.Plants.Include(p => p.PlantImages).AsQueryable();

                foreach (BasketCookieItemVM item in basketList)
                {
                    Plant plant = query.FirstOrDefault(s => s.Id == item.Id);
                    if (plant != null)
                    {
                        BasketItemVM basketItemVM = new BasketItemVM
                        {
                            plant = plant,
                            Count = item.Count
                        };
                        basketVM.BasketItemVMs.Add(basketItemVM);
                    }
                }
                decimal total = default;
                foreach (BasketItemVM items in basketVM.BasketItemVMs)
                {
                    total += items.plant.Price * items.Count;
                };
                basketVM.ToyalPrice = total;
                basketVM.Count = basketVM.BasketItemVMs.Count;
                return basketVM;

            }
            else
            {
                return null;
            }
        }
    }
}
