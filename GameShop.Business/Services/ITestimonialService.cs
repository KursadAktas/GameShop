using GameShop.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Business.Services
{
    public interface ITestimonialService
    {
        void AddTestimonial(AddTestimonialDto addProductDto);
    }
}
