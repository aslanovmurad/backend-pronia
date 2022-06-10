using AP204_Pronia.DAL;
using AP204_Pronia.Existent;
using AP204_Pronia.Models;
using AP204_Pronia.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP204_Pronia.Areas.proniaadmin.Controllers
{
    [Area("proniaadmin")]

    public class PlantController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment env;

        public PlantController(AppDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Plant> plants = await context.Plants.Include(p => p.PlantImages).ToListAsync();
            return View(plants);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Sizes = await context.Sizes.ToArrayAsync();
            ViewBag.Colors = await context.Colors.ToArrayAsync();
            ViewBag.Catagory = await context.Catagories.ToArrayAsync();


            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Plant plant)
        {
            ViewBag.Sizes = await context.Sizes.ToArrayAsync();
            ViewBag.Colors = await context.Colors.ToArrayAsync();
            ViewBag.Catagory = await context.Catagories.ToArrayAsync();

            if (!ModelState.IsValid) return View();
            if (plant.MainImage == null || plant.AnotherImage == null)
            {
                ModelState.AddModelError("", "Please choose mainImage or AnotherImage");
                return View();
            }
            if (!plant.MainImage.isExtent(1))
            {
                ModelState.AddModelError("MainImage", "Please choose mainImage max 1mb");
                return View();
            }
            foreach (var image in plant.AnotherImage)
            {
                if (!image.isExtent(1))
                {
                    ModelState.AddModelError("AnotherImage", "Please choose AnotherImage max 1mb");
                    return View();
                }
            }
            plant.PlantImages = new List<PlantImage>();
            PlantImage plantImage = new PlantImage
            {
                ImagePath = await plant.MainImage.FileCreate(env.WebRootPath, @"assets\images\website-images"),
                IsMain = true,
                Plants = plant
            };
            plant.PlantImages.Add(plantImage);
            foreach (var item in plant.AnotherImage)
            {
                PlantImage image = new PlantImage
                {
                    ImagePath = await plant.MainImage.FileCreate(env.WebRootPath, @"assets\images\website-images"),
                    IsMain = false,
                    Plants = plant
                };
                plant.PlantImages.Add(image);
            };
            plant.PlantCatagories = new List<PlantCatagory>();
            foreach (var item in plant.CatagoriesIds)
            {
                PlantCatagory catagory = new PlantCatagory
                {
                    Plant = plant,
                    CatagoryId = item
                };
                plant.PlantCatagories.Add(catagory);
            }

            await context.Plants.AddAsync(plant);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Sizes = await context.Sizes.ToArrayAsync();
            ViewBag.Colors = await context.Colors.ToArrayAsync();
            ViewBag.Catagory = await context.Catagories.ToArrayAsync();

            Plant plant = await context.Plants.Include(p => p.PlantImages).Include(p => p.PlantCatagories).FirstOrDefaultAsync(p => p.Id == id);
            if (plant == null) return NotFound();
            return View(plant);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, Plant plant)
        {

            ViewBag.Sizes = await context.Sizes.ToArrayAsync();
            ViewBag.Colors = await context.Colors.ToArrayAsync();
            ViewBag.Catagory = await context.Catagories.ToArrayAsync();


            Plant exsist = await context.Plants.Include(p => p.PlantImages).Include(p => p.PlantCatagories).FirstOrDefaultAsync(p => p.Id == id);
            if (exsist == null) return NotFound();

            if (plant.ImagesIds == null && plant.AnotherImage == null)
            {
                ModelState.AddModelError("", "you cannot delete all img becase adding another img");
                return View(exsist);
            }
            List<PlantImage> removeimage = exsist.PlantImages.Where(p => p.IsMain == false && !plant.ImagesIds.Contains(p.Id)).ToList();

            exsist.PlantImages.RemoveAll(p => removeimage.Any(rp => rp.Id == p.Id));

            List<PlantCatagory> removecatagory = exsist.PlantCatagories.Where(pc => !plant.CatagoriesIds.Contains(pc.CatagoryId)).ToList();
            exsist.PlantCatagories.RemoveAll(p => removecatagory.Any(pr => pr.Id == p.Id));

            foreach (var item in plant.CatagoriesIds)
            {
                PlantCatagory plantCatagor = exsist.PlantCatagories.FirstOrDefault(pr => pr.CatagoryId == item);
                if (plantCatagor != null)
                {
                    PlantCatagory plantCatagory = new PlantCatagory
                    {
                        PlantId = exsist.Id,
                        CatagoryId = item
                    };
                    exsist.PlantCatagories.Add(plantCatagory);

                }

            }

            foreach (var item in removeimage)
            {
                FileUtilities.FileDelete(env.WebRootPath, @"assets\images\website-images", item.ImagePath);
            }
            await context.SaveChangesAsync();

            if (plant.AnotherImage != null)
            {
                foreach (var item in plant.AnotherImage)
                {
                    PlantImage plantImage = new PlantImage
                    {
                        ImagePath = await item.FileCreate(env.WebRootPath, @"assets\images\website-images"),
                        IsMain = false,
                        PlantId = exsist.Id
                    };
                    exsist.PlantImages.Add(plantImage);
                }
            }

            context.Entry(exsist).CurrentValues.SetValues(plant);
            await context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Ditail(int id)
        {
            Plant plant = await context.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(d => d.Id == id);
            if (plant == null) return NotFound();
            return View(plant);
        }

        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Sizes = await context.Sizes.ToArrayAsync();
            ViewBag.Colors = await context.Colors.ToArrayAsync();
            ViewBag.Catagory = await context.Catagories.ToArrayAsync();

            Plant plant = await context.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(d => d.Id == id);
            if (plant == null) return NotFound();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> Deleted(int id,Plant plant)
        {
            ViewBag.Sizes = await context.Sizes.ToArrayAsync();
            ViewBag.Colors = await context.Colors.ToArrayAsync();
            ViewBag.Catagory = await context.Catagories.ToArrayAsync();

            Plant exsist = await context.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(d => d.Id == id);
            if (exsist == null) return NotFound();
            return View();

            context.Plants.Remove(plant);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
