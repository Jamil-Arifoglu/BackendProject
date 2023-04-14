
namespace Foxic.Entities
{
	public class Collection : BaseEntity
	{
		public string Name { get; set; }

		public List<Clothes> Clothes { get; set; }
		public Collection()
		{
			Clothes = new();
		}

	}
}
