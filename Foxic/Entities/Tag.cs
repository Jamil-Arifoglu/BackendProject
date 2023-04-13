using P230_Pronia.Entities;

namespace Foxic.Entities
{
	public class Tag : BaseEntity
	{
		public string Name { get; set; }

		public List<ClothesTag> ClothesTag { get; set; }

		public Tag()
		{
			ClothesTag = new();
		}
	}
}
