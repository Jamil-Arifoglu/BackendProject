using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Foxic.Areas.Foxic.Controllers
{
	[Area("FoxicAdmin")]
	public class ContactController : Controller
	{
		private readonly FoxicDbContext _context;

		public ContactController(FoxicDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			IEnumerable<Contact> instruction = _context.Contacts.AsEnumerable();
			return View(instruction);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Create")]
		[AutoValidateAntiforgeryToken]
		public IActionResult Creates(Contact newContact)
		{
			if (!ModelState.IsValid)
			{
				foreach (string message in ModelState.Values.SelectMany(v => v.Errors)
									.Select(e => e.ErrorMessage))
				{
					ModelState.AddModelError("", message);
				}

				return View();
			}
			bool isDuplicated = _context.Contacts.Any(d => d.Information == newContact.Information && d.Hours == newContact.Hours && d.ContactInformatin == newContact.ContactInformatin && d.Address == newContact.Address && d.Phone == newContact.Phone && d.QuickHelp == newContact.QuickHelp && d.Name == newContact.Name && d.URL == newContact.URL);
			if (isDuplicated)
			{
				ModelState.AddModelError("", "You cannot duplicate value");
				return View();
			}
			_context.Contacts.Add(newContact);
			_context.SaveChanges();


			return RedirectToAction(nameof(Index));
		}

		public IActionResult Edit(int id)
		{
			if (id == 0) return NotFound();
			Contact contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
			if (contact is null) return NotFound();
			return View(contact);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Edit(int id, Contact edited)
		{
			if (id == 0) return NotFound();
			Contact contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
			if (contact is null) return NotFound();

			bool duplicate = _context.Contacts.Any(d => d.Information == edited.Information && d.Hours == edited.Hours && d.ContactInformatin == edited.ContactInformatin && d.Address == edited.Address && d.Phone == edited.Phone && d.QuickHelp == edited.QuickHelp && d.Name == edited.Name && d.URL == edited.URL
			&& d.Information != edited.Information && d.Hours != edited.Hours && d.ContactInformatin != edited.ContactInformatin && d.Address != edited.Address && d.Phone != edited.Phone && d.QuickHelp != edited.QuickHelp && d.Name != edited.Name && d.URL != edited.URL
			);
			if (duplicate)
			{
				ModelState.AddModelError("", "You cannot duplicate category name");
				return View();
			}
			contact.Name = edited.Name;
			contact.Hours = edited.Hours;
			contact.Phone = edited.Phone;
			contact.Address = edited.Address;
			contact.QuickHelp = edited.QuickHelp;
			contact.Information = edited.Information;
			contact.ContactInformatin = edited.ContactInformatin;
			contact.URL = edited.URL;

			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Detail(int id)
		{

			if (id == 0) return NotFound();
			Contact contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
			if (contact is null) return NotFound();
			return View(contact);
		}

		public IActionResult Delete(int id)
		{
			if (id == 0) return NotFound();
			Contact contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
			if (contact is null) return NotFound();
			return View(contact);
		}


		[HttpPost]
		public IActionResult Delete(int id, Catagory delete)
		{
			if (id == 0) return NotFound();
			Contact contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
			if (contact is null) return NotFound();

			if (id == delete.Id)
			{
				_context.Contacts.Remove(contact);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(contact);
		}

	}
}
