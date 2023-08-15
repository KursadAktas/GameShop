using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Data.Entities
{
    public class Testimonial : BaseEntity
    {
        public string FullName { get; set; }
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }

    }
}
