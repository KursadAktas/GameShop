using System.ComponentModel.DataAnnotations;

namespace GameShop.WebUI.Model
{
    public class RegisterViewModel
    {
        [Display(Name ="Ad")] //form görüntüsü buradan TR diline çevrildi
        [MaxLength(30)] // maxsimum uzunluk harf
        [Required(ErrorMessage ="Ad alanı boş bırakılamaz.")] // girilmesi zorunlu alan
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        [MaxLength(30)]
        [Required(ErrorMessage = "Soyad alanı boş bırakılamaz.")]
        public string LastName { get; set; }

        [Display(Name = "E-Mail")]
        [MaxLength(50)]
        [Required(ErrorMessage = "E-mail alanı boş bırakılamaz.")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
       
        public string Password { get; set; }

        [Display(Name = "Şifre Tekar")]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [Compare(nameof(Password), ErrorMessage = "Şifreler eşleşmiyor.")] // şifre eşleşme kontrolü
        public string PasswordConfirm { get; set; }
    }
}
