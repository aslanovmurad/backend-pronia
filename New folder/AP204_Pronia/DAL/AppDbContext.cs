using AP204_Pronia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AP204_Pronia.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Setting>()
                .HasIndex(u => u.Key)
                .IsUnique();
            base.OnModelCreating(builder);
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<PlantImage> PlantImages { get; set; }
        public DbSet<PlantCatagory> PlantCatagories { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Setting> settings { get; set; }
    }
}
