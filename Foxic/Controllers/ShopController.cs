using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foxic.Utilities.Extensions;
using System.Numerics;

namespace Foxic.Controllers
{
	public class ShopController : Controller
	{
		readonly FoxicDbContext _context;

		public ShopController(FoxicDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			ViewBag.RelatedClothes = _context.Clothes
					.Include(p => p.ClothesImage).Include(t => t.ClothesTag).ThenInclude(tg => tg.Tag).Include(cs => cs.ClothesColorSize)
									   .ThenInclude(c => c.Color)
												.OrderByDescending(p => p.Id)
													.Take(1)
														.ToList();
			return View();
		}

		public IActionResult Details(int id)
		{
			IQueryable<Clothes> clother = _context.Clothes.AsNoTracking().AsQueryable();
			Clothes? clothes = clother.Include(ci => ci.ClothesImage)
						  .Include(ct => ct.ClothesGlobalTab).Include(ct => ct.Collection)

								 .Include(cs => cs.ClothesColorSize)
									   .ThenInclude(c => c.Color)
										   .Include(cs => cs.ClothesColorSize)
												.ThenInclude(s => s.Size).Include(t => t.ClothesTag).ThenInclude(th => th.Tag).AsSingleQuery().FirstOrDefault(x => x.Id == id);

			ViewBag.RelatedClothes = _context.Clothes
		.Include(p => p.ClothesImage).Include(t => t.ClothesTag).ThenInclude(tg => tg.Tag).Include(cs => cs.ClothesColorSize)
						   .ThenInclude(c => c.Color)
									.OrderByDescending(p => p.Id)
											.ToList();
			ViewBag.Relateds = RelatedClothes(clother, clothes, id);
			return View(clothes);
		}
		static List<Clothes> RelatedClothes(IQueryable<Clothes> queryable, Clothes clother, int id)
		{
			List<Clothes> relateds = new();

			clother.ClothesCatagory.ForEach(pc =>
			{
				List<Clothes> related = queryable
					.Include(p => p.ClothesImage)
						.Include(p => p.ClothesCatagory)
							.ThenInclude(pc => pc.Catagory)

									.AsEnumerable()
										.Where(
										p => p.ClothesCatagory.Contains(pc, new ClothesCategoryComparer())
										&& p.Id != id
										&& !relateds.Contains(p, new ClothesComparer())
										)
										.ToList();
				relateds.AddRange(related);
			});
			return relateds;
		}
	}
}
