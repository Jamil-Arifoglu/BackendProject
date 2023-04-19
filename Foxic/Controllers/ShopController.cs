using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foxic.Utilities.Extensions;
using System.Numerics;
using Microsoft.AspNetCore.Identity;
using System.Drawing;

namespace Foxic.Controllers
{
	public class ShopController : Controller
	{
		readonly FoxicDbContext _context;

		private readonly UserManager<User> _userManager;

		public ShopController(FoxicDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}
		public IActionResult Index()
		{
			ViewBag.RelatedClothes = _context.Clothes
					.Include(p => p.ClothesImage).Include(t => t.ClothesTag).ThenInclude(tg => tg.Tag).Include(cs => cs.ClothesColorSize)
									   .ThenInclude(c => c.Color)
												.OrderByDescending(p => p.Id)
													.Take(8)
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
												.ThenInclude(s => s.Size).Include(cc => cc.ClothesCatagory).ThenInclude(f => f.Catagory).Include(t => t.ClothesTag).ThenInclude(th => th.Tag).AsSingleQuery().FirstOrDefault(x => x.Id == id);

			ViewBag.RelatedClothes = _context.Clothes
		.Include(p => p.ClothesImage).Include(t => t.ClothesTag).ThenInclude(tg => tg.Tag).Include(cs => cs.ClothesColorSize)
						   .ThenInclude(c => c.Color)
									.OrderByDescending(p => p.Id)
											.ToList();
			ViewBag.Colors = clothes.ClothesColorSize.DistinctBy(p => p.ColorId).Select(p => new Entities.Color() { Id = p.ColorId, Name = p.Color.Name }).ToList();
			ViewBag.Sizes = _context.Sizes.ToList();
			ViewBag.Relateds = RelatedClothes(clother, clothes, id);
			return View(clothes);
		}

		[HttpPost]
		public async Task<IActionResult> AddComment(Comment comment, int id)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				Clothes clother = await _context.Clothes.Include(pt => pt.Comments).FirstOrDefaultAsync(p => p.Id == id);
				User user = await _userManager.FindByNameAsync(User.Identity.Name);
				Comment newcomment = new Comment()
				{
					Text = comment.Text,
					User = user,
					CreationTime = DateTime.UtcNow,
					Clothes = clother

				};
				user.Comments.Add(newcomment);
				clother.Comments.Add(newcomment);
				await _context.Comments.AddAsync(newcomment);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Details), new { id });
			}
		}
		[HttpPost]


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
