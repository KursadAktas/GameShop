using GameShop.Business.Dto;
using GameShop.Business.Services;
using GameShop.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult List()
        {
            var listCategotyDtos = _categoryService.GetCategories();

            var viewModel = listCategotyDtos.Select(x=> new CategoryListViewModel
            {
                Id = x.Id,
                Name = x.Name
                
                
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View("form" , new CategoryFormViewModel()); //sayfa boş bir model ile açıldı.
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var updadeCategoryDto = _categoryService.GetCategory(id);
            var viewModel = new CategoryFormViewModel()
            {
                Id = updadeCategoryDto.ID,
                Name = updadeCategoryDto.Name,
                Discription = updadeCategoryDto.Description

            };

            return View("form",viewModel);
        }


        [HttpPost]
        public IActionResult Save(CategoryFormViewModel formData)
        {
            if (!ModelState.IsValid) //form şartları kontrolü için model state
            {
                return View("Form" , formData);
            }

            if (formData.Id == 0) //yeni kayıt için id yoksa
            {
                var addCategoryDto = new AddCategoryDto()
                {
                    Name = formData.Name,
                    Desciription = formData.Discription
                };
                var result =  _categoryService.AddCategory(addCategoryDto);
                if (result)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.ErrorMessage = "Bu isimde bir kategori mevcuttur.";
                    return View("Form", formData);

                }

            }
            else // id varsa güncelleme işlemi.
            {
                var updateCategoryDto = new UpdateCategoryDto()
                {
                    ID = formData.Id,
                    Name = formData.Name,
                    Description = formData.Discription
                };
                _categoryService.UpdateCategory(updateCategoryDto);
                return RedirectToAction("List");
            }
        }
            public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("List");
        }
    }
}
