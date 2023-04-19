using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Foxic.Areas.Foxic.Controllers
{
	[Area("FoxicAdmin")]
	//[Authorize(Roles = "Admin, Moderator")]
	public class TagController : Controller
	{
		private readonly FoxicDbContext _context;

		public TagController(FoxicDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			IEnumerable<Tag> tags = _context.Tagas.AsEnumerable();
			return View(tags);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Creates(Tag newTags)
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
			bool isDuplicated = _context.Tagas.Any(c => c.Name == newTags.Name);
			if (isDuplicated)
			{
				ModelState.AddModelError("", "You cannot duplicate value");
				return View();
			}
			_context.Tagas.Add(newTags);
			_context.SaveChanges();


			return RedirectToAction(nameof(Index));
		}

		public IActionResult Edit(int id)
		{
			if (id == 0) return NotFound();
			Tag tags = _context.Tagas.FirstOrDefault(c => c.Id == id);
			if (tags is null) return NotFound();
			return View(tags);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Edit(int id, Tag edited)
		{
			if (id == 0) return NotFound();
			Tag tags = _context.Tagas.FirstOrDefault(c => c.Id == id);
			if (tags is null) return NotFound();
			bool duplicate = _context.Tagas.Any(c => c.Name == edited.Name);
			if (duplicate)
			{
				ModelState.AddModelError("", "You cannot duplicate tags name");
				return View();
			}
			tags.Name = edited.Name;
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Detail(int id)
		{


			if (id == 0) return NotFound();
			Tag tags = _context.Tagas.FirstOrDefault(c => c.Id == id);
			if (tags is null) return NotFound();
			return View(tags);
		}

		public IActionResult Delete(int id)
		{
			if (id == 0) return NotFound();
			Tag tags = _context.Tagas.FirstOrDefault(c => c.Id == id);
			if (tags is null) return NotFound();
			return View(tags);
		}


		[HttpPost]
		public IActionResult Delete(int id, Tag delete)
		{

			if (id == 0) return NotFound();
			Tag tags = _context.Tagas.FirstOrDefault(c => c.Id == id);
			if (tags is null) return NotFound();



			if (id == delete.Id)
			{
				_context.Tagas.Remove(tags);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(tags);
		}

	}
}
