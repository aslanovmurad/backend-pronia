using AP204_Pronia.DAL;
using AP204_Pronia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AP204_Pronia.Areas.proniaadmin.Controllers
{
    [Area("proniaadmin")]

    public class SizeController : Controller
    {

        private readonly AppDbContext context;

        public SizeController(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Size> sizes = await context.Sizes.ToListAsync();
            return View(sizes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Size size)
        {
            if (!ModelState.IsValid) return View();
            await context.Sizes.AddAsync(size);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            Size sizes = await context.Sizes.FirstOrDefaultAsync(s => s.Id == id);
            if (sizes == null) return NoContent();
            return View(sizes);
        }


        public async Task<IActionResult> Edit(int id)
        {
            Size sizes = await context.Sizes.FirstOrDefaultAsync(s => s.Id == id);
            if (sizes == null) return NoContent();
            return View(sizes);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id,Size size) 
        {
            Size existedsize = await context.Sizes.FirstOrDefaultAsync(s=>s.Id==id);
            if (existedsize == null) return NotFound();
            if (id != size.Id) return BadRequest();
            //existedsize.Name = size.Name;
            return Content("Existed.id" + existedsize.Id + "size.id" + size.Id);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





        public async Task<IActionResult> Delete(int id)
        {
            Size size = await context.Sizes.FirstOrDefaultAsync(s => s.Id == id);
            if (size == null) return NotFound();
            return View(size);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> Deletesize(int id) 
        {

            Size size = await context.Sizes.FirstOrDefaultAsync(s => s.Id == id);
            if (size == null) return NotFound();

            context.Sizes.Remove(size);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
