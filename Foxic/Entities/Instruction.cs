using P230_Pronia.Entities;

namespace Foxic.Entities
{
	public class Instruction : BaseEntity
	{
		public string Text { get; set; }
		public List<Clothes> Clothes { get; set; }
		public Instruction()
		{
			Clothes = new();
		}
	}
}
