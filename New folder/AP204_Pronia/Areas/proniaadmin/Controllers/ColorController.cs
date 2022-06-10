using AP204_Pronia.DAL;
using AP204_Pronia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AP204_Pronia.Areas.proniaadmin.Controllers
{
    [Area("proniaadmin")]

    public class ColorController : Controller
    {
        private readonly AppDbContext context;

        public ColorController(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Color> colors = await context.Colors.ToListAsync();
            return View(colors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Color color)
        {
            if (!ModelState.IsValid) return View();
            await context.Colors.AddAsync(color);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            Color color = await context.Colors.FirstOrDefaultAsync(s => s.Id == id);
            if (color == null) return NotFound();
            return View(color);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Color color = await context.Colors.FirstOrDefaultAsync(d => d.Id == id);
            if (color == null) return NotFound();
            return View(color);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, Color color)
        {
            Color colorexist = await context.Colors.FirstOrDefaultAsync(d => d.Id == id);
            if (colorexist == null) return NotFound();
            if (id != color.Id) return BadRequest();
            return Content("Exist.id" + colorexist.Id + "color.id" + color.Id);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Color color = await context.Colors.FirstOrDefaultAsync(b => b.Id == id);
            if (color == null) return NotFound();
            return View(color);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> Deletecolor(int id)
        {
            Color color = await context.Colors.FirstOrDefaultAsync(b => b.Id == id);
            if (color == null) return NotFound();

            context.Colors.Remove(color);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
