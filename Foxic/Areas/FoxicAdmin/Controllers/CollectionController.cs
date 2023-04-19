using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Foxic.Areas.Foxic.Controllers
{
	[Area("FoxicAdmin")]
	//[Authorize(Roles = "Admin, Moderator")]
	public class CollectionController : Controller
	{
		private readonly FoxicDbContext _context;

		public CollectionController(FoxicDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			IEnumerable<Collection> collections = _context.Collections.AsEnumerable();
			return View(collections);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Creates(Collection newCollections)
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
			bool isDuplicated = _context.Tagas.Any(c => c.Name == newCollections.Name);
			if (isDuplicated)
			{
				ModelState.AddModelError("", "You cannot duplicate value");
				return View();
			}
			_context.Collections.Add(newCollections);
			_context.SaveChanges();


			return RedirectToAction(nameof(Index));
		}

		public IActionResult Edit(int id)
		{
			if (id == 0) return NotFound();
			Collection collections = _context.Collections.FirstOrDefault(c => c.Id == id);
			if (collections is null) return NotFound();
			return View(collections);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Edit(int id, Collection edited)
		{

			if (id == 0) return NotFound();
			Collection collections = _context.Collections.FirstOrDefault(c => c.Id == id);
			if (collections is null) return NotFound();

			bool duplicate = _context.Collections.Any(c => c.Name == edited.Name);
			if (duplicate)
			{
				ModelState.AddModelError("", "You cannot duplicate tags name");
				return View();
			}
			collections.Name = edited.Name;
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Detail(int id)
		{

			if (id == 0) return NotFound();
			Collection collections = _context.Collections.FirstOrDefault(c => c.Id == id);
			if (collections is null) return NotFound();
			return View(collections);
		}

		public IActionResult Delete(int id)
		{
			if (id == 0) return NotFound();
			Collection collections = _context.Collections.FirstOrDefault(c => c.Id == id);
			if (collections is null) return NotFound();
			return View(collections);
		}


		[HttpPost]
		public IActionResult Delete(int id, Collection delete)
		{

			if (id == 0) return NotFound();
			Collection collections = _context.Collections.FirstOrDefault(c => c.Id == id);
			if (collections is null) return NotFound();




			if (id == delete.Id)
			{
				_context.Collections.Remove(collections);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(collections);
		}

	}
}
