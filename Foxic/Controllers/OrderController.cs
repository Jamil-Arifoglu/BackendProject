using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace Foxic.Controllers
{
    public class OrderController : Controller
    {

        private readonly FoxicDbContext _context;
        private readonly UserManager<User> _userManager;

        public OrderController(FoxicDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //[HttpPost]
        //public async Task<IActionResult> AddBasket(int clotherId, Clothes basketClother)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        ClothesColorSize? clother = _context.ClothesColorSizes.Include(p => p.Clothes).FirstOrDefault(p => p.ClothesId == clotherId && p.SizeId == basketClother.AddCart.SizeId && p.ColorId == basketClother.AddCart.ColorId);
        //        if (clother is null) return NotFound();

        //        User user = await _userManager.FindByNameAsync(User.Identity.Name);
        //        Basket? userActiveBasket = _context.Baskets
        //                                        .Include(b => b.User)
        //                                           .FirstOrDefault(b => b.User.Id == user.Id && !b.IsOrdered) ?? new Basket();

        //        BasketItem item = new()
        //        {
        //            ClothesColorSize = clother.ClothesId,
        //            SaleQuantity = basketClothes.AddCart.Quantity,
        //            UnitPrice = plant.Plant.Price
        //        };
        //        userActiveBasket.BasketItems.Add(item);
        //        userActiveBasket.User = user;
        //        userActiveBasket.TotalPrice = userActiveBasket.BasketItems.Sum(p => p.SaleQuantity * p.UnitPrice);
        //        _context.Baskets.Add(userActiveBasket);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //}

        //public IActionResult GetSizes(int plantId, int colorId)
        //{
        //    List<PlantSizeColor> plantSizeColor = _context.PlantSizeColors
        //                                               .Include(p => p.Size).Where(p => p.PlantId == plantId && p.ColorId == colorId).ToList();
        //    if (plantSizeColor is null) return Json(new { status = 404 });
        //    var sizes = plantSizeColor.Select(p => new { Id = p.SizeId, p.Size.Name }).ToList();
        //    return Json(sizes);
        //}
    }
}

