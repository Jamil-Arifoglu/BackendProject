using Foxic.DAL;
using Foxic.Entities;
using Foxic.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Foxic.Areas.Foxic.Controllers
{
	[Area("Foxic")]
	public class ColorController : Controller
	{
		private readonly FoxicDbContext _context;
		private readonly IWebHostEnvironment _env;

		public ColorController(FoxicDbContext context, IWebHostEnvironment env)
		{
			_context = context;
			_env = env;
		}
		public IActionResult Index()
		{
			IEnumerable<Color> colors = _context.Colors.AsEnumerable();
			return View(colors);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Create(Color newColor)
		{
			if (newColor.Image is null)
			{
				ModelState.AddModelError("Image", "Please Select Image");
				return View();
			}
			if (!newColor.Image.IsValidFile("image/"))
			{
				ModelState.AddModelError("Image", "Please Select Image Tag");
				return View();
			}
			if (!newColor.Image.IsValidLength(2))
			{
				ModelState.AddModelError("Image", "Please Select Image which size max 2MB");
				return View();
			}

			string imagesFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
			newColor.ColorPath = await newColor.Image.CreateImage(imagesFolderPath, "products");
			_context.Colors.Add(newColor);
			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}
		public IActionResult Edit(int id)
		{
			if (id == 0) return NotFound();

			Color colors = _context.Colors.FirstOrDefault(s => s.Id == id);
			if (colors is null) return BadRequest();
			return View(colors);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Color edited)
		{
			if (id != edited.Id) return BadRequest();
			Color colors = _context.Colors.FirstOrDefault(s => s.Id == id);
			if (!ModelState.IsValid) return View(colors);

			_context.Entry(colors).CurrentValues.SetValues(edited);

			if (edited.Image is not null)
			{
				string imagesFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
				string filePath = Path.Combine(imagesFolderPath, "products", colors.ColorPath);
				FileUpload.DeleteImage(filePath);
				colors.ColorPath = await edited.Image.CreateImage(imagesFolderPath, "products");
			}
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Detail(int id)
		{
			if (id == 0) return NotFound();
			Color colors = _context.Colors.FirstOrDefault(s => s.Id == id);
			if (colors is null) return NotFound();
			return View(colors);
		}
		public IActionResult Delete(int id)
		{

			if (id == 0) return NotFound();
			Color colors = _context.Colors.FirstOrDefault(s => s.Id == id);
			if (colors is null) return NotFound();
			return View(colors);
		}

		[HttpPost]
		public IActionResult Delete(int id, Slider delete)
		{

			if (id == 0) return NotFound();
			Color colors = _context.Colors.FirstOrDefault(s => s.Id == id);
			if (colors is null) return NotFound();


			if (id == delete.Id)
			{
				_context.Colors.Remove(colors);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(colors);
		}
	}
}
