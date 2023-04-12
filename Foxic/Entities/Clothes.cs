using P230_Pronia.Entities;

namespace Foxic.Entities
{
	public class Clothes : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public decimal? DiscountPrice { get; set; }
		public decimal Discount { get; set; }
		public string discountRate { get; set; }
		public bool? IsStok { get; set; }
		public int? Stok { get; set; }
		public decimal Price { get; set; }
		public string SKU { get; set; }
		public string Barcode { get; set; }
		public string Instruction { get; set; }

		public bool Availability { get; set; }

		public ClothesGlobalTab ClothesGlobalTab { get; set; }
		public List<ClothesImage> ClothesImage { get; set; }
		public List<ClothesColorSize> ClothesColorSize { get; set; }
		public List<ClothesCollection> ClothesCollection { get; set; }
		public List<ClothesTag> ClothesTag { get; set; }
		public Clothes()
		{
			ClothesColorSize = new();
			ClothesImage = new();
		}
	}

}
