using GameShop.Business.Dto;
using GameShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Business.Services
{
    public interface IProductService
    {
        void AddProduct(AddProductDto addProductDto);

        List<ListProductDto> GetProducts();

        EditProductDto GetProductById(int id);

        void EditProduct(EditProductDto editProductDto);

        void DeleteProduct(int id);

        List<ListProductDto> GetProductsByCategoryId(int? categoryId);

        DetailsProductDto DetailsProductById(int id);

  
       
    }
}