using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Data.Entities
{
    public class Cart : BaseEntity
    {
        public decimal? UnitPrice { get; set; }
       
        public string UserName { get; set; }
        public int UnitInStock { get; set; }
        public string? ImagePath { get; set; }
        public string Description { get; set; }
    



        public int UserId { get; set; }
        public UserEntity UserEntity { get; set; }

        public int ProductId { get; set; }
        public ProductEntity ProductEntity { get; set; }
    }
}
