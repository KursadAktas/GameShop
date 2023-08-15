using GameShop.Business.Dto;
using GameShop.Business.Services;
using GameShop.Data.Entities;
using GameShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Business.Managers
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepository<CategoryEntity> _categoryRepository; //DI
        public CategoryManager(IRepository<CategoryEntity> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public bool AddCategory(AddCategoryDto addCategoryDto)
        {
            var hasCategory = _categoryRepository.GetAll(x=>x.Name.ToLower() == addCategoryDto.Name.ToLower()).ToList(); //Query
            if (hasCategory.Any())
            {
                return false;
            }
            var categoryEntity = new CategoryEntity()
            {
                Name = addCategoryDto.Name,
                Description = addCategoryDto.Desciription
            };

            _categoryRepository.Add(categoryEntity);
            return true;
        }

        public void DeleteCategory(int id)
        {
            _categoryRepository.Delete(id);
        }

        public List<ListCategotyDto> GetCategories()
        {
            var categoryEntities = _categoryRepository.GetAll().OrderBy(x => x.Name);
            var categoryDtoList = categoryEntities.Select(x => new ListCategotyDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList(); //veri aktarımı yapıldı listdto newlendi
            return categoryDtoList;
        }

        public UpdateCategoryDto GetCategory(int id)
        {
           var categoriEntity = _categoryRepository.GetById(id);
            var updateCategoryDto = new UpdateCategoryDto()
            {
                ID = categoriEntity.Id,
                Name = categoriEntity.Name,
                Description = categoriEntity.Description
            };
            return updateCategoryDto;
        }

        public void UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var categoryEntity = _categoryRepository.GetById(updateCategoryDto.ID); // güncelleme için ıd yi bul.
            categoryEntity.Name = updateCategoryDto.Name;
            categoryEntity.Description = updateCategoryDto.Description; //bilgileri ekle
            _categoryRepository.Update(categoryEntity);
        }
    }
}
