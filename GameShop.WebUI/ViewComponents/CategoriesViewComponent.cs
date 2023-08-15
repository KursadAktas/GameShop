using GameShop.Business.Services;
using GameShop.WebUI.Model;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.WebUI.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            var categoriDto = _categoryService.GetCategories();
            var viewModel = categoriDto.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return View(viewModel);
        }
    }
}
