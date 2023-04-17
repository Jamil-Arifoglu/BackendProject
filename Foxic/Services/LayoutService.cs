using Foxic.DAL;
using Foxic.Entities;
using Newtonsoft.Json;
using P230_Pronia.ViewModels.Cookie;

namespace Foxic.Services
{
	public class LayoutService
	{
		private readonly FoxicDbContext _context;
		private readonly IHttpContextAccessor _accessor;

		public LayoutService(FoxicDbContext context, IHttpContextAccessor accessor)
		{
			_context = context;
			_accessor = accessor;
		}
		public List<Setting> GetSettings()
		{
			List<Setting> settings = _context.Settings.ToList();
			return settings;
		}

		public CookieBasketVM GetBasket()
		{
			var cookies = _accessor.HttpContext.Request.Cookies["basket"];
			CookieBasketVM basket = new();
			if (cookies is not null)
			{
				basket = JsonConvert.DeserializeObject<CookieBasketVM>(cookies);
				foreach (CookieBasketItemVM item in basket.CookieBasketItemVMs)
				{
					Clothes clother = _context.Clothes.FirstOrDefault(p => p.Id == item.Id);
					if (clother is null)
					{
						basket.CookieBasketItemVMs.Remove(item);
						basket.TotalPrice -= item.Quantity * item.Price;
					}
				}
			}
			return basket;

		}

		public List<Clothes> GetClothes()
		{

			List<Clothes> clother = _context.Clothes.ToList();
			return clother;
		}



	}
}