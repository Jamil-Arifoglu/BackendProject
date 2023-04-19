namespace Foxic.Entities
{
	public class Comment : BaseEntity
	{
		public string Text { get; set; }
		public DateTime CreationTime { get; set; }
		public User User { get; set; }
		public Clothes Clothes { get; set; }



	}
}

