using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Foxic.Areas.Foxic.Controllers
{
    [Area("Foxic")]
    //[Authorize(Roles = "Admin, Moderator")]
    public class ClothesGlobalTabController : Controller
    {
        private readonly FoxicDbContext _context;

        public ClothesGlobalTabController(FoxicDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<ClothesGlobalTab> tabs = _context.ClothesGlobalTabs.AsEnumerable();
            return View(tabs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Creates(ClothesGlobalTab newTabs)
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
            bool isDuplicated = _context.ClothesGlobalTabs.Any(c => c.GlobalTab == newTabs.GlobalTab);
            if (isDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }
            _context.ClothesGlobalTabs.Add(newTabs);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            ClothesGlobalTab tabs = _context.ClothesGlobalTabs.FirstOrDefault(c => c.Id == id);
            if (tabs is null) return NotFound();
            return View(tabs);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, ClothesGlobalTab edited)
        {
            if (id == 0) return NotFound();
            ClothesGlobalTab tabs = _context.ClothesGlobalTabs.FirstOrDefault(c => c.Id == id);
            if (tabs is null) return NotFound();
            bool duplicate = _context.ClothesGlobalTabs.Any(c => c.GlobalTab == edited.GlobalTab);
            if (duplicate)
            {
                ModelState.AddModelError("", "You cannot duplicate tags name");
                return View();
            }
            tabs.GlobalTab = edited.GlobalTab;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int id)
        {


            if (id == 0) return NotFound();
            ClothesGlobalTab tabs = _context.ClothesGlobalTabs.FirstOrDefault(c => c.Id == id);
            if (tabs is null) return NotFound();
            return View(tabs);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            ClothesGlobalTab tabs = _context.ClothesGlobalTabs.FirstOrDefault(c => c.Id == id);
            if (tabs is null) return NotFound();
            return View(tabs);
        }


        [HttpPost]
        public IActionResult Delete(int id, ClothesGlobalTab delete)
        {

            if (id == 0) return NotFound();
            ClothesGlobalTab tabs = _context.ClothesGlobalTabs.FirstOrDefault(c => c.Id == id);
            if (tabs is null) return NotFound();



            if (id == delete.Id)
            {
                _context.ClothesGlobalTabs.Remove(tabs);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(tabs);
        }

    }
}
