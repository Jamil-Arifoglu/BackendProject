

namespace Foxic.Entities
{
	public class ClothesTag : BaseEntity
	{
		public int TagId { get; set; }
		public Tag Tag { get; set; }
		public Clothes Clothes { get; set; }
	}
}
