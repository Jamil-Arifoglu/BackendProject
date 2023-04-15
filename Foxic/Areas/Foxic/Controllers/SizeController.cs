using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Foxic.Areas.Foxic.Controllers
{
    [Area("Foxic")]
    public class SizeController : Controller
    {
        private readonly FoxicDbContext _context;

        public SizeController(FoxicDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Size> sizes = _context.Sizes.AsEnumerable();
            return View(sizes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Creates(Size newSize)
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
            bool isDuplicated = _context.Catagory.Any(c => c.Name == newSize.Name);
            if (isDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }
            _context.Sizes.Add(newSize);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            Size sizes = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (sizes is null) return NotFound();
            return View(sizes);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, Size edited)
        {
            if (id == 0) return NotFound();
            Size sizes = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (sizes is null) return NotFound();

            bool duplicate = _context.Sizes.Any(c => c.Name == edited.Name);
            if (duplicate)
            {
                ModelState.AddModelError("", "You cannot duplicate category name");
                return View();
            }
            sizes.Name = edited.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int id)
        {

            if (id == 0) return NotFound();
            Size sizes = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (sizes is null) return NotFound();
            return View(sizes);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            Size sizes = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (sizes is null) return NotFound();
            return View(sizes);
        }


        [HttpPost]
        public IActionResult Delete(int id, Catagory delete)
        {
            if (id == 0) return NotFound();
            Size sizes = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (sizes is null) return NotFound();


            if (id == delete.Id)
            {
                _context.Sizes.Remove(sizes);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(sizes);
        }
    }
}
