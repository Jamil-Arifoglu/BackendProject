using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Foxic.DAL;
using Foxic.Entities;
using Foxic.Migrations;


namespace Foxic.Areas.ProniaAdmin.Controllers
{
	[Area("FoxicAdmin")]
	//[Authorize(Roles = "Admin, Moderator")]
	public class CategoryController : Controller
	{
		private readonly FoxicDbContext _context;

		public CategoryController(FoxicDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			IEnumerable<Catagory> categories = _context.Catagory.AsEnumerable();
			return View(categories);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Create")]
		[AutoValidateAntiforgeryToken]
		public IActionResult Creates(Catagory newCategory)
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
			bool isDuplicated = _context.Catagory.Any(c => c.Name == newCategory.Name);
			if (isDuplicated)
			{
				ModelState.AddModelError("", "You cannot duplicate value");
				return View();
			}
			_context.Catagory.Add(newCategory);
			_context.SaveChanges();


			return RedirectToAction(nameof(Index));
		}

		public IActionResult Edit(int id)
		{
			if (id == 0) return NotFound();
			Catagory category = _context.Catagory.FirstOrDefault(c => c.Id == id);
			if (category is null) return NotFound();
			return View(category);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult Edit(int id, Catagory edited)
		{
			if (id != edited.Id) return BadRequest();
			Catagory category = _context.Catagory.FirstOrDefault(c => c.Id == id);
			if (category is null) return NotFound();
			bool duplicate = _context.Catagory.Any(c => c.Name == edited.Name);
			if (duplicate)
			{
				ModelState.AddModelError("", "You cannot duplicate category name");
				return View();
			}
			category.Name = edited.Name;
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Detail(int id)
		{

			if (id == 0) return NotFound();
			Catagory category = _context.Catagory.FirstOrDefault(c => c.Id == id);
			if (category is null) return NotFound();
			return View(category);
		}

		public IActionResult Delete(int id)
		{
			if (id == 0) return NotFound();
			Catagory category = _context.Catagory.FirstOrDefault(c => c.Id == id);
			if (category is null) return NotFound();

			return View(category);
		}


		[HttpPost]
		public IActionResult Delete(int id, Catagory delete)
		{
			if (id == 0) return NotFound();
			Catagory category = _context.Catagory.FirstOrDefault(c => c.Id == id);
			if (category is null) return NotFound();


			if (id == delete.Id)
			{
				_context.Catagory.Remove(category);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(category);
		}

	}
}
