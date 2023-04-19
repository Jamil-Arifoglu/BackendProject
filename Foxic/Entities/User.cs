using Microsoft.AspNetCore.Identity;

namespace Foxic.Entities
{
	public class User : IdentityUser
	{
		public string Fullname { get; set; }



		public List<Comment> Comments { get; set; }

		public User()
		{

			Comments = new();
		}
	}
}
