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
    
    public class TestimonialManager : ITestimonialService
    {
        private readonly IRepository<Testimonial> _testimonialRepository;

        public TestimonialManager(IRepository<Testimonial> testimonialRepository)
        {
            _testimonialRepository = testimonialRepository;
        }

            public void AddTestimonial(AddTestimonialDto addTestimonialDto)
            {
                var testimonialEntity = new Testimonial()
                {
                        FullName=addTestimonialDto.FullName,
                        Comment = addTestimonialDto.Comment,
                        CreatedDate = addTestimonialDto.CreatedDate,
                    

                };

                _testimonialRepository.Add(testimonialEntity);

            }

        }
    }

