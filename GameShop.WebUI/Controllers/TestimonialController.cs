using GameShop.Business.Dto;
using GameShop.Business.Services;
using GameShop.Data.Context;
using GameShop.Data.Entities;
using GameShop.WebUI.Extensions.GameShop.WebUI.Extensions;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameShop.WebUI.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save(Testimonial formData)
        {
            var testimonialDto = new AddTestimonialDto()
            {
                
                FullName =User.GetUserName(),
                Comment = formData.Comment,
                CreatedDate = DateTime.Now,
            };
            _testimonialService.AddTestimonial(testimonialDto);
            return RedirectToAction();
        }
    }
}
