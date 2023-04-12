using P230_Pronia.Entities;

namespace Foxic.Entities
{
	public class Slider : BaseEntity
	{
		public string ImagePath { get; set; }
		public string ClothesName { get; set; }
		public string Text { get; set; }
		public string Buttontext { get; set; }

		public byte Order { get; set; }
		public bool? IsVideo { get; set; }


	}
}
