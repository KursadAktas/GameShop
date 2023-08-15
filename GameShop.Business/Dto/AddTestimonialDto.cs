using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Business.Dto
{
    public class AddTestimonialDto
    {
        public string FullName { get; set; }
        public string Comment { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
