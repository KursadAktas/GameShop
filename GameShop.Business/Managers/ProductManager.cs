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
    public class ProductManager : IProductService
    {
        private readonly IRepository<ProductEntity> _productRepository;
      
        public ProductManager(IRepository<ProductEntity> productRepository, IRepository<Cart> cardRepository)
        {
            _productRepository = productRepository;
            
        }

        

        
        

        public void AddProduct(AddProductDto addProductDto)
        {
            var productEntity = new ProductEntity()
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                UnitInStock = addProductDto.UnitInStock,
                UnitPrice = addProductDto.UnitPrice,
                CategoryId = addProductDto.CategoryId,
                ImagePath = addProductDto.ImagePath
            };

            _productRepository.Add(productEntity);

        }

        public void DeleteProduct(int id)
        {
            _productRepository.Delete(id);
        }

        public DetailsProductDto DetailsProductById(int id)
        {
            var detailProductEntity = _productRepository.GetById(id);
            var detailProductDto = new DetailsProductDto()
            {
                Id = detailProductEntity.Id,
                Name = detailProductEntity.Name,
                Description = detailProductEntity.Description,
                UnitInStock = detailProductEntity.UnitInStock,
                UnitPrice = detailProductEntity.UnitPrice,
                CategoryId = detailProductEntity.CategoryId,
                ImagePath= detailProductEntity.ImagePath
            };
            return detailProductDto;
        }

        public void EditProduct(EditProductDto editProductDto)
        {
            var productEntity = _productRepository.GetById(editProductDto.Id); // id ile eşleşen nesnenin tamamını çektim.

            productEntity.Name = editProductDto.Name;
            productEntity.Description = editProductDto.Description;
            productEntity.UnitPrice = editProductDto.UnitPrice;
            productEntity.UnitInStock = editProductDto.UnitInStock;
            productEntity.CategoryId = editProductDto.CategoryId;

            if (editProductDto.ImagePath is not null)
            {
                productEntity.ImagePath = editProductDto.ImagePath;
            }
            // Bu If'i yazmasam, editProductDto aracılığı ile View'den gelen null olan imagePath bilgisi, veritabanımdaki görsel bilgisi üzerine yazılır, böylelikle elimde olan görseli silmiş olurum. Bunu istemiyorum !

            _productRepository.Update(productEntity);


        }

        public EditProductDto GetProductById(int id)
        {
            var productEntity = _productRepository.GetById(id);

            var editProductDto = new EditProductDto()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                UnitInStock = productEntity.UnitInStock,
                UnitPrice = productEntity.UnitPrice,
                CategoryId = productEntity.CategoryId,
                ImagePath = productEntity.ImagePath
            };

            return editProductDto;
        }

        public List<ListProductDto> GetProducts()
        {
            var productEntities = _productRepository.GetAll().OrderBy(x => x.Category.Name).ThenBy(x => x.Name);
            // Önce kategori adına sonra ürün adına göre sılara.

            var productDtoList = productEntities.Select(x => new ListProductDto
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                UnitInStock = x.UnitInStock,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                ImagePath = x.ImagePath
            }).ToList();

            return productDtoList;
        }

        public List<ListProductDto> GetProductsByCategoryId(int? categoryId)
        {

            if (categoryId.HasValue) // is not null 
            {
                var productEntities = _productRepository.GetAll(x => x.CategoryId == categoryId).OrderBy(x => x.Name);
                // Gönderdiğim categoryId ile CategoryId verisi işleşenleri, isimlerine göre sıralayarak getir.


                var productDtos = productEntities.Select(x => new ListProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UnitInStock = x.UnitInStock,
                    UnitPrice = x.UnitPrice,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    ImagePath = x.ImagePath
                }).ToList();

                return productDtos;

            }
            else
            {
                return GetProducts();
                // GetProducts metodunu çalıştır. O metot zaten categoryId'den bağımsız bir şekilde, bütün ürün bilgilerini geriye dönüyor.
            }

        }

      
    }
}
