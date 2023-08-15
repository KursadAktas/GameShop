using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Business.Dto
{
    public class AddCategoryDto
    {
        public string Name { get; set; }

        public string Desciription { get; set; }

        //id sıfır olduğundan taşınmıyor.
    }
}
