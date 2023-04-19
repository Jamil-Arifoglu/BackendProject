using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Foxic.Areas.Foxic.Controllers
{
	[Area("FoxicAdmin")]
	public class SettingController : Controller
	{
		private readonly FoxicDbContext _context;

		public SettingController(FoxicDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			IEnumerable<Setting> settings = _context.Settings.AsEnumerable();
			return View(settings);
		}
		public IActionResult Create()
		{

			return View();
		}
		[HttpPost]
		[ActionName("Create")]
		[AutoValidateAntiforgeryToken]
		public IActionResult Create(Setting newSetting)
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
			bool isDuplicated = _context.Settings.Any(c => c.key == newSetting.key && c.value == newSetting.value);
			if (isDuplicated)
			{
				ModelState.AddModelError("", "You cannot duplicate value");
				return View();
			}
			_context.Settings.Add(newSetting);
			_context.SaveChanges();


			return RedirectToAction(nameof(Index));
		}
		public IActionResult Edit(int id)
		{
			if (id == 0) return NotFound();
			Setting settings = _context.Settings.FirstOrDefault(c => c.Id == id);
			if (settings is null) return NotFound();
			return View(settings);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Edit(int id, Setting edited)
		{
			if (id == 0) return NotFound();
			Setting settings = _context.Settings.FirstOrDefault(c => c.Id == id);
			if (settings is null) return NotFound();

			bool duplicate = _context.Settings.Any(c => c.key == edited.key && c.value == edited.value && c.key != edited.key && c.value != edited.value);
			if (duplicate)
			{
				ModelState.AddModelError("", "You cannot duplicate category name");
				return View();
			}
			settings.key = edited.key;
			settings.value = edited.value;
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Detail(int id)
		{

			if (id == 0) return NotFound();
			Setting settings = _context.Settings.FirstOrDefault(c => c.Id == id);
			if (settings is null) return NotFound();
			return View(settings);
		}
		[HttpPost]
		public IActionResult Delete(int id, Catagory delete)
		{
			if (id == 0) return NotFound();
			Setting settings = _context.Settings.FirstOrDefault(c => c.Id == id);
			if (settings is null) return NotFound();


			if (id == delete.Id)
			{
				_context.Settings.Remove(settings);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(settings);
		}
	}
}
