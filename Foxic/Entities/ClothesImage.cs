

namespace Foxic.Entities
{
	public class ClothesImage : BaseEntity
	{
		public string Path { get; set; }

		public bool? IsMain { get; set; }
		public Clothes Clothes { get; set; }
	}
}
