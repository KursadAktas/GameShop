using GameShop.Business.Services;
using GameShop.WebUI.Model;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.WebUI.ViewComponents
{
    public class ProductsViewComponent : ViewComponent  
    {
        private readonly IProductService _productService;
        public ProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke(int? categoryId = null)
        {
            var productDto = _productService.GetProductsByCategoryId(categoryId);

            var viewModel = productDto.Select(x => new ProductViewModel
            {

                Id = x.Id,
                Name = x.Name,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                CategoryName = x.CategoryName,
                ImagePath = x.ImagePath
            }).ToList();

            return View(viewModel);

        }
           
        
    }
}
