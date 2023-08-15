using GameShop.Business.Dto;
using GameShop.Business.Services;
using GameShop.WebUI.Model;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService; //DI
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var detailProductDto = _productService.DetailsProductById(id);
            var viewModel = new ProductDetailsViewModel()
            {
                Id = detailProductDto.Id,
                Name = detailProductDto.Name,
                UnitInStock = detailProductDto.UnitInStock,
                UnitInPrice = detailProductDto.UnitPrice,
              ImagePath = detailProductDto.ImagePath,
            };
         
            return View(viewModel);
        }

    }
}
