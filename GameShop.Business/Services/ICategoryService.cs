using GameShop.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Business.Services
{
    public interface ICategoryService
    {
        bool AddCategory(AddCategoryDto addCategoryDto); //bool olarak döneceğiz

        List<ListCategotyDto> GetCategories();
        UpdateCategoryDto GetCategory(int id);

        void UpdateCategory(UpdateCategoryDto updateCategoryDto);

        void DeleteCategory(int id);
    }
}
