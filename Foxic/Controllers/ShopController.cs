﻿using Microsoft.AspNetCore.Mvc;

namespace Foxic.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {

            return View();
        }
    }
}
