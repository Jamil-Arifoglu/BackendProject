
using Foxic.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foxic.ViewModels
{
    public class ClothesVM
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 20)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        [NotMapped]
        public ICollection<int> CategoryIds { get; set; } = null!;
        [NotMapped]
        public ICollection<int> TagIds { get; set; } = null!;
        [NotMapped]
        public ICollection<int> SizeId { get; set; } = null!;
        [NotMapped]
        public ICollection<int> ColorId { get; set; } = null!;

        [NotMapped]
        public int InstructionId { get; set; }
        [NotMapped]
        public int CollectionId { get; set; }
        [NotMapped]
        public int ClothesGlobalTabId { get; set; }
        [NotMapped]
        public IFormFile? MainPhoto { get; set; }
        //[NotMapped]
        //public IFormFile? FalsePhoto { get; set; }
        [NotMapped]
        public ICollection<IFormFile>? Images { get; set; }
        [NotMapped]
        public ICollection<ClothesImage>? SpecificImages { get; set; }
        [NotMapped]

        public ICollection<int>? ImageIds { get; set; }
        public ICollection<ClothesColorSize> ClothesColorSizes { get; set; }
        public string? ClothesColorSizeId { get; set; }


        public string? ColorSizeQuantity { get; set; }

    }
}
