using System.ComponentModel.DataAnnotations;

namespace GameShop.WebUI.Areas.Admin.Models
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }


        [Display(Name = "Ad")]
        [Required(ErrorMessage ="Kategori Adını Girmek Zorunludur.")]
        public string Name { get; set; }


        [Display(Name = "Özellikler")]
        public string? Discription { get; set; }
    }
}
