

namespace Foxic.Entities
{
	public class Size : BaseEntity
	{
		public string Name { get; set; }
		public List<ClothesColorSize> ClothesColorSize { get; set; }

		public Size()
		{
			ClothesColorSize = new();
		}
	}
}
