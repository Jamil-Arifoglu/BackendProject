using Foxic.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Foxic.DAL
{
    public class FoxicDbContext : IdentityDbContext
    {
        public FoxicDbContext(DbContextOptions<FoxicDbContext> options) : base(options)
        {


        }
        public DbSet<Slider> sliders { get; set; }
        public DbSet<Collection> collections { get; set; }
        public DbSet<Clothes> clothes { get; set; }
        public DbSet<Tag> Tagas { get; set; }
        public DbSet<ClothesCollection> ClothesCollections { get; set; }
        public DbSet<ClothesColorSize> ClothesColorSizes { get; set; }
        public DbSet<ClothesImage> ClothesImages { get; set; }
        public DbSet<ClothesGlobalTab> ClothesGlobalTabs { get; set; }
        public DbSet<ClothesTag> ClothesTags { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Setting> Settings { get; set; }


    }
}
