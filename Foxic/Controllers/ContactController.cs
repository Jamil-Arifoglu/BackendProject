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
            IEnumerable<Contact> settings = _context.Contacts.AsEnumerable();
            return View(settings);
        }
    }
}
