
using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Foxic.Controllers
{
	public class FoxicController : Controller
	{
		private FoxicDbContext _context;

		public FoxicController(FoxicDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			List<Slider> slider = _context.sliders.OrderBy(s => s.Order).ToList();
			ViewBag.RelatedPlants = _context.clothes
.Include(p => p.ClothesImage)
.Include(pc => pc.ClothesCollection).ThenInclude(c => c.Collection)
					  .Take(8)
					  .ToList();

			return View(slider);
		}


	}
}