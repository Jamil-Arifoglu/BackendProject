using Foxic.DAL;
using Newtonsoft.Json;

namespace P230_Pronia.Services
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
        //public List<Setting> GetSettings()
        //{
        //    List<Setting> settings = _context.Settings.ToList();
        //    return settings;
        //}

        //public CookieBasketVM GetBasket()
        //{
        //    var cookies = _accessor.HttpContext.Request.Cookies["basket"];
        //    CookieBasketVM basket=new();
        //    if (cookies is not null)
        //    {
        //        basket = JsonConvert.DeserializeObject<CookieBasketVM>(cookies);
        //        foreach (CookieBasketItemVM item in basket.CookieBasketItemVMs)
        //        {
        //            Plant plant = _context.Plants.FirstOrDefault(p => p.Id == item.Id);
        //            if (plant is null)
        //            {
        //                basket.CookieBasketItemVMs.Remove(item);
        //                basket.TotalPrice -= item.Quantity * item.Price;
        //            }
        //        }
        //    }
        //    return basket;

        //}

        //public List<Plant> GetPlants()
        // {

        //     List<Plant> plant = _context.Plants.ToList();
        //     return plant;
        // }



    }
}