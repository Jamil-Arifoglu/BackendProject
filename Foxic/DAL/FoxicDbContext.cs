using Foxic.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Foxic.ViewModels;

namespace Foxic.DAL
{
    public class FoxicDbContext : IdentityDbContext<User>
    {
        public FoxicDbContext(DbContextOptions<FoxicDbContext> options) : base(options)
        {


        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Tag> Tagas { get; set; }
        public DbSet<ClothesCatagory> ClothesCatagory { get; set; }
        public DbSet<ClothesColorSize> ClothesColorSizes { get; set; }
        public DbSet<ClothesImage> ClothesImages { get; set; }
        public DbSet<ClothesGlobalTab> ClothesGlobalTabs { get; set; }
        public DbSet<ClothesTag> ClothesTags { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public DbSet<Catagory> Catagory { get; set; }

        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        public DbSet<Basket> Baskets { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<Order> Orders { get; set; }




    }
}
