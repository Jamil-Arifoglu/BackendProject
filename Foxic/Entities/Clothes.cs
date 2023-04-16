

using Foxic.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foxic.Entities
{
    public class Clothes : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal? DiscountPrice { get; set; }


        public string SKU { get; set; }
        public string Barcode { get; set; }
        public int InstructionId { get; set; }
        public Instruction Instruction { get; set; }
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }
        public int ClothesGlobalTabId { get; set; }
        public ClothesGlobalTab ClothesGlobalTab { get; set; }

        public List<ClothesImage> ClothesImage { get; set; }
        public List<ClothesColorSize> ClothesColorSize { get; set; }
        public List<ClothesCatagory> ClothesCatagory { get; set; }
        public List<ClothesTag> ClothesTag { get; set; }
        [NotMapped]
        public AddCartVM AddCart { get; set; }


        public Clothes()
        {
            ClothesColorSize = new();
            ClothesImage = new();
            ClothesCatagory = new();
            ClothesTag = new();
            Instruction = new();

        }
    }

}
