using AP204_Pronia.DAL;
using AP204_Pronia.Models;
using AP204_Pronia.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP204_Pronia.Controllers
{
    public class PlantController : Controller
    {
        private readonly AppDbContext context;

        public PlantController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = context.Plants.AsQueryable();
            ViewBag.Totalpage = Math.Ceiling(((decimal)await query.CountAsync()) / 3);
            ViewBag.Currentpage = page;
            HomeVB homeVB = new HomeVB
            {
                Plants = await context.Plants.Include(p => p.PlantImages).Skip((page - 1) * 3).Take(2).ToListAsync()
            };

            return View(homeVB);
        }


        public async Task<IActionResult> Addbasket(int id)
        {
            Plant plant = await context.Plants.FirstOrDefaultAsync(s => s.Id == id);
            if (plant == null) return NotFound();

            string item = HttpContext.Request.Cookies["basket"];
            List<BasketCookieItemVM> basket;
            //BasketVM basket;
            //string itemstr;
            if (string.IsNullOrEmpty(item))
            {
                basket = new List<BasketCookieItemVM>();
                BasketCookieItemVM basketCookieItemVM = new BasketCookieItemVM
                {
                    Id = plant.Id,
                    Count = 1
                };
                //BasketItemVM basketItemVM = new BasketItemVM
                //{
                //    plant = plant,
                //    Count = 1
                //};
                basket.Add(basketCookieItemVM);
                //basket.BasketItemVMs.Add(basketItemVM);
                item = JsonConvert.SerializeObject(basket);

            }
            else
            {
                basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(item);

                BasketCookieItemVM cookie = basket.FirstOrDefault(c => c.Id == plant.Id);
                //BasketItemVM itemVM = basket.BasketItemVMs.FirstOrDefault(i => i.plant.Id == id);
                if (cookie == null)
                {
                    BasketCookieItemVM basketCookieItemVM = new BasketCookieItemVM
                    {
                        Id = plant.Id,
                        Count = 1
                    };
                    basket.Add(basketCookieItemVM);
                    //BasketItemVM itemVM1 = new BasketItemVM
                    //{
                    //    plant = plant,
                    //    Count = 1
                    //};
                    //basket.BasketItemVMs.Add(itemVM1);
                }
                else
                {
                    cookie.Count++;
                }
                //decimal total = default;
                //foreach (BasketCookieItemVM items in basket)
                //{
                //    total += items.plant.Price * items.Count;
                //};
                //basket.ToyalPrice = total;
                //basket.Count = basket.BasketItemVMs.Count;
                item = JsonConvert.SerializeObject(basket);

            }
            HttpContext.Response.Cookies.Append("basket", item);
            return RedirectToAction("Index", "Home");
        }
        public ContentResult Basket()
        {
            return Content(HttpContext.Request.Cookies["basket"]);

        }

        public async Task<IActionResult> Remove(int id)
        {
            Plant plant = await context.Plants.FirstOrDefaultAsync(s => s.Id == id);
            if (plant == null) return NotFound();
            return View(plant);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Remove")]
        public async Task<IActionResult> Remov(int id,Plant plant)
        {
            Plant plants = await context.Plants.FirstOrDefaultAsync(s => s.Id == id);
            if (plants == null) return NotFound();

            plant.Id = plants.Id;
            context.Remove(plant);
            context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
