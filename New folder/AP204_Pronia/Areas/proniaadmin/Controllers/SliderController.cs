using AP204_Pronia.DAL;
using AP204_Pronia.Existent;
using AP204_Pronia.Models;
using AP204_Pronia.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AP204_Pronia.Areas.proniaadmin.Controllers
{
    [Area("proniaadmin")]
    public class SliderController : Controller
    {

        private readonly AppDbContext context;
        private readonly IWebHostEnvironment _evn;

        public SliderController(AppDbContext _context, IWebHostEnvironment evn)
        {
            context = _context;
            _evn = evn;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await context.Sliders.ToListAsync();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            if (slider.Photo != null)
            {
                //if (!slider.Photo.ContentType.Contains("image"))
                //{
                //    ModelState.AddModelError("Photo", "please choos suported file");
                //    return View();
                //}
                //if (slider.Photo.Length > 1024 * 1024)
                //{
                //    ModelState.AddModelError("Photo", "please choos image file");
                //    return View();
                //}

                if (!slider.Photo.isExtent(1))
                {
                    ModelState.AddModelError("Photo", "please choos suported file");
                    return View();
                }

                string filestrim = await slider.Photo.FileCreate(_evn.WebRootPath,@"assets\image\website-images");
                string path = Path.Combine(_evn.WebRootPath, "assets", "images", "website-images");
                string fullpath = Path.Combine(path, filestrim);

                using (FileStream file = new FileStream(fullpath, FileMode.Create))
                {
                    await slider.Photo.CopyToAsync(file);
                }

                slider.Image = filestrim;
                await context.Sliders.AddAsync(slider);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("Photo", "Plesae choos file");
                return View();
            }


        }

        public async Task<IActionResult> Detail(int id)
        {
            Slider slider = await context.Sliders.FirstOrDefaultAsync(d => d.Id == id);
            if (slider == null) return View();
            return View(slider);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Slider slider = await context.Sliders.FirstOrDefaultAsync(d => d.Id == id);
            if (slider == null) return View();
            return View(slider);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, Slider slider)
        {
            Slider sliders = await context.Sliders.FirstOrDefaultAsync(d => d.Id == id);

            if (sliders == null) return View();
            if (id != slider.Id) return BadRequest();
            //return Content("sliders.id" + sliders.Id + "slider.id" + slider.Id);
            context.Entry(sliders).CurrentValues.SetValues(slider);
            await context.SaveChangesAsync();
            return RedirectToAction();

        }


        //public async Task<IActionResult> Delete(int id)
        //{
        //    Slider slider = await context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        //    if (slider == null) return NotFound();
        //    return View();
        //}
        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public async Task<IActionResult> Deleted(int id)
        //{
        //    Slider slider = await context.Sliders.FirstOrDefaultAsync();
        //    if (slider == null) return NotFound();

        //    FileStream fileStream = new FileStream(context.)

        //}


    }
}

