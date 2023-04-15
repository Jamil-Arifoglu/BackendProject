using Foxic.Entities;

namespace Foxic.Entities
{
	public class ClothesCatagory : BaseEntity
	{
		public int CatagoryId { get; set; }
		public Catagory Catagory { get; set; }

		public Clothes Clothes { get; set; }

	}
}
