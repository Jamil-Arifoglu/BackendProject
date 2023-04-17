
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

        public IActionResult Index(int id)
        {
            List<Slider> slider = _context.Sliders.OrderBy(s => s.Order).ToList();

            ViewBag.RelatedClothes = _context.Clothes
                    .Include(p => p.ClothesImage)
                                                .OrderByDescending(p => p.Id)
                                                    .Take(8)
                                                        .ToList();

            return View(slider);
        }


    }
}