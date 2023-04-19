using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Foxic.Controllers
{
	public class ContactController : Controller
	{
		private readonly FoxicDbContext _context;

		public ContactController(FoxicDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			List<Contact> settings = _context.Contacts.ToList();
			return View(settings);
		}
	}
}
