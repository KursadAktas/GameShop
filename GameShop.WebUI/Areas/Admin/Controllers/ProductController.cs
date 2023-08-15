using GameShop.Business.Dto;
using GameShop.Business.Services;
using GameShop.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService; //DI
        private readonly IWebHostEnvironment _environment;
        private readonly IProductService _productService;
        public ProductController(ICategoryService categoryService, IWebHostEnvironment environment, IProductService productService)
        {
            _categoryService = categoryService;
            _environment = environment;
            _productService = productService;
        }
        public IActionResult List()
        {
            var productDtos = _productService.GetProducts();

            var viewModel = productDtos.Select(x => new ListProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                ImagePath = x.ImagePath
            }).ToList();
            return View(viewModel);
        
        }

        [HttpGet]
        public IActionResult New() 
        {
            ViewBag.Categories = _categoryService.GetCategories(); //viewbag ile taşıdık
            return View("Form" , new ProductFormViewModel());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editProductDto = _productService.GetProductById(id);
            var viewModel = new ProductFormViewModel() //kategoriler geldi
            {
                Id = editProductDto.Id,
                Name = editProductDto.Name,
                CategoryId = editProductDto.CategoryId,
                UnitInStock = editProductDto.UnitInStock,
                UnitPrice = editProductDto.UnitPrice,
                Description = editProductDto.Description

            };
            ViewBag.ImagePath = editProductDto.ImagePath;// resim yüklenirken boş gelmesin eski görsel.
            ViewBag.Categories = _categoryService.GetCategories(); //viewbag ile taşıdık
            return View("form",viewModel);
        }




        [HttpPost]
        public IActionResult Save(ProductFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                if (formData.CategoryId == 0)
                {
                    ViewBag.CatError = "Kategori Seçilmek zorunda.";
                }
                ViewBag.Categories = _categoryService.GetCategories();
                return View("Form", formData);
                // Formu her açışımda ViewBag'i dolu göndermeliyim çünkü view kısmında viewbag içerisindeki listede döngü ile veri çekme var. Yoksa nullexception fırlatır.
            }

            var newFileName = "";

            if (formData.File is not null)
            {
                var allowedFileTypes = new string[] { "image/jpeg", "image/jpg", "image/png", "image/jfif" }; // izin vereceğim dosya içerikleri.

                var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".jfif" }; // izin vereceğim dosya uzantıları.


                var fileContentType = formData.File.ContentType; //dosyanın içeriği
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.File.FileName); // uzantısız dosya ismi
                var fileExtension = Path.GetExtension(formData.File.FileName); // dosya uzantısı.

                // dosya tipi benim listemdekilerden biri değilse
                if (!allowedFileTypes.Contains(fileContentType) ||
                    !allowedFileExtensions.Contains(fileExtension))
                {
                    ViewBag.FileError = "Dosya formatı veya içeriği hatalı";
                    // TODO : BU UYARIYI VİEW'DE GÖSTERİN.

                    ViewBag.Categories = _categoryService.GetCategories();
                    return View("Form", formData);

                }

                newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;
                // Aynı isimde iki dosya yüklendiğinde hata vermesin diye, birbiriyle asla eşleşmeyecek şekilde her dosya adına unique bir metin ilavesi (guid) yapıyorum.

                var folderPath = Path.Combine("images", "products");
                // images/products

                // _environment.WebRootPath -> wwwroot'a kadar olan kısım.
                var wwwrootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
                // ...wwwroot/images/products


                var wwwrootFilePath = Path.Combine(wwwrootFolderPath, newFileName);
                //...wwwroot/images/products/urunGorseli-12313123dsadwa.jpg

                Directory.CreateDirectory(wwwrootFolderPath); // Eğer images/products klasörleri açılmadıysa, aç demek.

                using (var fileStream = new FileStream(wwwrootFilePath, FileMode.Create))
                {
                    formData.File.CopyTo(fileStream);
                }
                // asıl dosya kopyalamanın yapıldığı kısım.

                // using içerisinde new'lenen filestream nesnesi, scope boyunca yaşar, scope bitiminde silinir.

            }

            if (formData.Id == 0) // EKLEME
            {
                var addProductDto = new AddProductDto()
                {
                    Name = formData.Name,
                    Description = formData.Description,
                    UnitPrice = formData.UnitPrice,
                    UnitInStock = formData.UnitInStock,
                    CategoryId = formData.CategoryId,
                    ImagePath = newFileName
                };

                _productService.AddProduct(addProductDto);
                return RedirectToAction("list");

            }
            else // GÜNCELLEME 
            {
                var editProductDto = new EditProductDto()
                {
                    Id = formData.Id,
                    Name = formData.Name,
                    Description = formData.Description,
                    UnitInStock = formData.UnitInStock,
                    UnitPrice = formData.UnitPrice,
                    CategoryId = formData.CategoryId
                };

                if (formData.File is not null)
                {
                    editProductDto.ImagePath = newFileName;
                }
                // bu kontrolü yaparak gitmeliyim çünkü yeni bir dosya seçilmediyse ve null gönderildiyse, DB'de olan görselin üzerine null atayıp, görseli kaybetmek istemiyorum.

                _productService.EditProduct(editProductDto);
                return RedirectToAction("List");
            }



        }

        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("list");
        }
    }


}
