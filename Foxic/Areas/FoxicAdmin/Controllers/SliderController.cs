using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Mvc;
using Foxic.Utilities.Extensions;

namespace Foxic.Areas.Foxic.Controllers
{
	[Area("FoxicAdmin")]
	public class SliderController : Controller
	{
		private readonly FoxicDbContext _context;
		private readonly IWebHostEnvironment _env;

		public SliderController(FoxicDbContext context, IWebHostEnvironment env)
		{
			_context = context;
			_env = env;
		}
		public IActionResult Index()
		{
			IEnumerable<Slider> sliders = _context.Sliders.AsEnumerable();
			return View(sliders);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Slider newSlider)
		{
			if (newSlider.Image is null)
			{
				ModelState.AddModelError("Image", "Please Select Image");
				return View();
			}
			if (!newSlider.Image.IsValidFile("image/"))
			{
				ModelState.AddModelError("Image", "Please Select Image Tag");
				return View();
			}
			if (!newSlider.Image.IsValidLength(2))
			{
				ModelState.AddModelError("Image", "Please Select Image which size max 2MB");
				return View();
			}


			string imagesFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
			newSlider.ImagePath = await newSlider.Image.CreateImage(imagesFolderPath, "slider");
			_context.Sliders.Add(newSlider);
			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}
		public IActionResult Edit(int id)
		{
			if (id == 0) return NotFound();

			Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
			if (slider is null) return BadRequest();
			return View(slider);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Slider edited)
		{
			if (id != edited.Id) return BadRequest();
			Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
			if (!ModelState.IsValid) return View(slider);

			_context.Entry(slider).CurrentValues.SetValues(edited);

			if (edited.Image is not null)
			{
				string imagesFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
				string filePath = Path.Combine(imagesFolderPath, "slider", slider.ImagePath);
				FileUpload.DeleteImage(filePath);
				slider.ImagePath = await edited.Image.CreateImage(imagesFolderPath, "slider");
			}
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Detail(int id)
		{
			if (id == 0) return NotFound();
			Slider sliders = _context.Sliders.FirstOrDefault(s => s.Id == id);
			if (sliders is null) return NotFound();
			return View(sliders);
		}
		public IActionResult Delete(int id)
		{
			if (id == 0) return NotFound();
			Slider sliders = _context.Sliders.FirstOrDefault(s => s.Id == id);
			if (sliders is null) return NotFound();

			return View(sliders);
		}

		[HttpPost]
		public IActionResult Delete(int id, Slider delete)
		{
			if (id == 0) return NotFound();
			Slider sliders = _context.Sliders.FirstOrDefault(s => s.Id == id);
			if (sliders is null) return NotFound();

			if (id == delete.Id)
			{
				_context.Sliders.Remove(sliders);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(sliders);
		}
	}
}
