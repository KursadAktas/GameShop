using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Business.Dto
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int UnitInStock { get; set; }

        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
    }
}
