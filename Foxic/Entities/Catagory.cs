

namespace Foxic.Entities
{
	public class Catagory : BaseEntity
	{
		public string Name { get; set; }

		public List<ClothesCatagory> ClothesCatagory { get; set; }
		public Catagory()
		{
			ClothesCatagory = new();
		}
	}
}
