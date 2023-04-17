using Foxic.DAL;
using Foxic.Entities;
using Foxic.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foxic.ViewModels;


namespace Foxic.Areas.Foxic.Controllers
{
    [Area("Foxic")]
    public class ClothesController : Controller
    {
        private readonly FoxicDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ClothesController(FoxicDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            IEnumerable<Clothes> model = _context.Clothes.Include(p => p.ClothesImage).Include(i => i.Instruction).Include(ct => ct.Collection)
                                                         .Include(p => p.ClothesColorSize).ThenInclude(p => p.Size)
                                                         .Include(p => p.ClothesColorSize).ThenInclude(p => p.Color)
                                                         .AsNoTracking().AsEnumerable();
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.ClothesGlobalTabs = _context.ClothesGlobalTabs.AsEnumerable();
            ViewBag.Instruction = _context.Instructions.AsEnumerable();
            ViewBag.Catagory = _context.Catagory.AsEnumerable();
            ViewBag.Tags = _context.Tagas.AsEnumerable();
            ViewBag.Sizes = _context.Sizes.AsEnumerable();
            ViewBag.Colors = _context.Colors.AsEnumerable();
            ViewBag.Collections = _context.Collections.AsEnumerable();

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create(ClothesVM? newclothe)

        {
            return Json((newclothe));
            ViewBag.ClothesGlobalTabs = _context.ClothesGlobalTabs.AsEnumerable();
            ViewBag.Instruction = _context.Instructions.AsEnumerable();
            ViewBag.Catagory = _context.Catagory.AsEnumerable();
            ViewBag.Tags = _context.Tagas.AsEnumerable();
            ViewBag.Sizes = _context.Sizes.AsEnumerable();
            ViewBag.Colors = _context.Colors.AsEnumerable();
            ViewBag.Collections = _context.Collections.AsEnumerable();
            TempData["InvalidImages"] = string.Empty;
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!newclothe.MainPhoto.IsValidFile("image/"))
            {
                ModelState.AddModelError(string.Empty, "Please choose image file");
                return View();
            }
            if (!newclothe.MainPhoto.IsValidLength(1))
            {
                ModelState.AddModelError(string.Empty, "Please choose image which size is maximum 1MB");
                return View();
            }

            Clothes clother = new()
            {
                Name = newclothe.Name,
                Description = newclothe.Description,
                ShortDescription = newclothe.ShortDescription,
                Price = newclothe.Price,
                Discount = newclothe.Discount,
                DiscountPrice = newclothe.DiscountPrice,
                SKU = newclothe.SKU,
                Barcode = newclothe.Barcode,
                InstructionId = newclothe.InstructionId,
                CollectionId = newclothe.CollectionId,
                ClothesGlobalTabId = newclothe.ClothesGlobalTabId


            };
            string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
            foreach (var image in newclothe.Images)
            {
                if (!image.IsValidFile("image/") || !image.IsValidLength(1))
                {
                    TempData["InvalidImages"] += image.FileName;
                    continue;
                }
                ClothesImage clothesImage = new()
                {
                    IsMain = false,
                    Path = await image.CreateImage(imageFolderPath, "products")
                };
                clother.ClothesImage.Add(clothesImage);
            }

            ClothesImage main = new()
            {
                IsMain = true,
                Path = await newclothe.MainPhoto.CreateImage(imageFolderPath, "products")
            };
            clother.ClothesImage.Add(main);



            string[] colorSizeQuantities = newclothe.ColorSizeQuantity.Split(',');
            foreach (string colorSizeQuantity in colorSizeQuantities)
            {
                string[] datas = colorSizeQuantity.Split('-');
                ClothesColorSize plantSizeColor = new()
                {
                    SizeId = int.Parse(datas[0]),
                    ColorId = int.Parse(datas[1]),
                    Quantity = int.Parse(datas[2])
                };
                clother.ClothesColorSize.Add(plantSizeColor);

            }

            foreach (int id in newclothe.CategoryIds)
            {
                ClothesCatagory category = new()
                {
                    CatagoryId = id
                };
                clother.ClothesCatagory.Add(category);
            }
            foreach (int id in newclothe.TagIds)
            {
                ClothesTag tag = new()
                {
                    TagId = id
                };
                clother.ClothesTag.Add(tag);
            }



            _context.Clothes.Add(clother);
            _context.SaveChanges();
            return RedirectToAction("Index", "Clothes");
        }


        public IActionResult Edit(int id)
        {
            if (id == 0) return BadRequest();
            ClothesVM? model = EditedPlant(id);

            ViewBag.ClothesGlobalTabs = _context.ClothesGlobalTabs.AsEnumerable();
            ViewBag.Instruction = _context.Instructions.AsEnumerable();
            ViewBag.Catagory = _context.Catagory.AsEnumerable();
            ViewBag.Tags = _context.Tagas.AsEnumerable();
            ViewBag.Sizes = _context.Sizes.AsEnumerable();
            ViewBag.Colors = _context.Colors.AsEnumerable();
            ViewBag.Collections = _context.Collections.AsEnumerable();


            if (model is null) return BadRequest();
            _context.SaveChanges();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ClothesVM edited)
        {
            ViewBag.ClothesGlobalTabs = _context.ClothesGlobalTabs.AsEnumerable();
            ViewBag.Instruction = _context.Instructions.AsEnumerable();
            ViewBag.Catagory = _context.Catagory.AsEnumerable();
            ViewBag.Tags = _context.Tagas.AsEnumerable();
            ViewBag.Sizes = _context.Sizes.AsEnumerable();
            ViewBag.Colors = _context.Colors.AsEnumerable();
            ViewBag.Collections = _context.Collections.AsEnumerable();
            TempData["InvalidImages"] = string.Empty;

            ClothesVM? model = EditedPlant(id);

            Clothes? clother = await _context.Clothes.Include(p => p.ClothesImage).Include(p => p.ClothesCatagory).Include(p => p.ClothesTag).FirstOrDefaultAsync(p => p.Id == id);
            if (clother is null) return BadRequest();

            IEnumerable<string> removables = clother.ClothesImage.Where(p => !edited.ImageIds.Contains(p.Id)).Select(i => i.Path).AsEnumerable();
            string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
            foreach (string removable in removables)
            {
                string path = Path.Combine(imageFolderPath, "products", removable);
                await Console.Out.WriteLineAsync(path);
                Console.WriteLine(FileUpload.DeleteImage(path));
            }


            if (edited.MainPhoto is not null)
            {
                await AdjustPlantPhotos(edited.MainPhoto, clother, true);
            }
            //else if (edited.Images is not null)
            //{
            //    await AdjustPlantPhotos(edited.Images, clother, null);
            //}
            string[] colorSizeQuantities = edited.ColorSizeQuantity.Split(',');
            foreach (string colorSizeQuantity in colorSizeQuantities)
            {
                string[] datas = colorSizeQuantity.Split('-');
                int sizeId = int.Parse(datas[0]);
                int colorId = int.Parse(datas[1]);
                int quantity = int.Parse(datas[2]);

                ClothesColorSize existing = clother.ClothesColorSize
                    .FirstOrDefault(psc => psc.SizeId == sizeId && psc.ColorId == colorId);

                if (existing != null)
                {
                    existing.Quantity = quantity;
                }
                else
                {
                    ClothesColorSize clothesSizeColor = new()
                    {
                        SizeId = sizeId,
                        ColorId = colorId,
                        Quantity = quantity
                    };
                    clother.ClothesColorSize.Add(clothesSizeColor);
                }
            }

            clother.ClothesImage.RemoveAll(p => !edited.ImageIds.Contains(p.Id));
            if (edited.Images is not null)
            {
                foreach (var item in edited.Images)
                {
                    if (!item.IsValidFile("image/") || !item.IsValidLength(1))
                    {
                        TempData["InvalidImages"] += item.FileName;
                        continue;
                    }
                    ClothesImage plantImage = new()
                    {
                        IsMain = false,
                        Path = await item.CreateImage(imageFolderPath, "products")
                    };
                    clother.ClothesImage.Add(plantImage);
                }
            }
            clother.Name = edited.Name;
            clother.Description = edited.Description;
            clother.ShortDescription = edited.ShortDescription;
            clother.Price = edited.Price;
            clother.Discount = edited.Discount;
            clother.DiscountPrice = edited.DiscountPrice;
            clother.SKU = edited.SKU;
            clother.ShortDescription = edited.ShortDescription;


            clother.ClothesGlobalTabId = edited.ClothesGlobalTabId;
            clother.Barcode = edited.Barcode;
            clother.CollectionId = edited.CollectionId;


            clother.ClothesCatagory.Clear();
            clother.ClothesTag.Clear();

            foreach (int categoryId in edited.CategoryIds ?? new List<int>())
            {
                Catagory category = await _context.Catagory.FirstOrDefaultAsync(c => c.Id == categoryId);
                if (category != null)
                {
                    ClothesCatagory plantCategory = new ClothesCatagory { Catagory = category };
                    clother.ClothesCatagory.Add(plantCategory);
                }
            }

            foreach (int tagId in edited.TagIds ?? new List<int>())
            {
                Tag tag = await _context.Tagas.FirstOrDefaultAsync(t => t.Id == tagId);
                if (tag != null)
                {
                    ClothesTag ClothesTag = new ClothesTag { Tag = tag };
                    clother.ClothesTag.Add(ClothesTag);
                }
            }
            _context.Clothes.Add(clother);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private ClothesVM? EditedPlant(int id)
        {
            ClothesVM? model = _context.Clothes.Include(p => p.ClothesCatagory)
                                            .Include(p => p.ClothesTag)
                                            .Include(p => p.ClothesImage)
                                            .Include(P => P.ClothesColorSize)
                                            .Select(p =>
                                                new ClothesVM
                                                {
                                                    Id = p.Id,
                                                    Name = p.Name,
                                                    SKU = p.SKU,
                                                    Description = p.Description,
                                                    Price = p.Price,
                                                    DiscountPrice = p.DiscountPrice,
                                                    Discount = p.Discount,
                                                    ClothesGlobalTabId = p.ClothesGlobalTabId,
                                                    CategoryIds = p.ClothesCatagory.Select(pc => pc.CatagoryId).ToList(),
                                                    TagIds = p.ClothesTag.Select(pc => pc.TagId).ToList(),
                                                    SpecificImages = p.ClothesImage.Select(p => new ClothesImage
                                                    {
                                                        Id = p.Id,
                                                        Path = p.Path,
                                                        IsMain = p.IsMain
                                                    }).ToList()

                                                })
                                                .FirstOrDefault(p => p.Id == id);
            return model;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="plant"></param>
        /// <param name="isMain">If IsMain attribute is true that is mean you want to change Main photo, if IsMain attribute is null that is mean you want to change HoverPhoto</param>
        /// <returns></returns>
        private async Task AdjustPlantPhotos(IFormFile image, Clothes clother, bool? isMain)
        {
            string photoPath = clother.ClothesImage.FirstOrDefault(p => p.IsMain == isMain).Path;
            string imagesFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "skins", "fashion");
            string filePath = Path.Combine(imagesFolderPath, "products", photoPath);
            FileUpload.DeleteImage(filePath);
            clother.ClothesImage.FirstOrDefault(p => p.IsMain == isMain).Path = await image.CreateImage(imagesFolderPath, "products");
        }

        public IActionResult Detail(int id)
        {
            if (id == 0) return NotFound();
            Clothes clothes = _context.Clothes.FirstOrDefault(s => s.Id == id);
            if (clothes is null) return NotFound();
            return View(clothes);

        }



    }
}
