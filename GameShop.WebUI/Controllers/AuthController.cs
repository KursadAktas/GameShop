using GameShop.Business.Dto;
using GameShop.Business.Services;
using GameShop.WebUI.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.Win32;
using System.Security.Claims;

namespace GameShop.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService; //Dependency injection

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }



        //kimlik doğrulama
        [HttpGet]
        [Route("kayit-ol")]  //Routing gönderilen URL’in controllerda bulunan action ile eşleştirir ve arama motorlarını anlayabileceği bir url oluşturur.
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [Route("kayit-ol")]
        public IActionResult Register (RegisterViewModel formData)
        {
            if (!ModelState.IsValid) //veri kayıp edilmiyor eğer istenenler geçerli değilse. aynı veriler forma geri geliyor.
            {
                return View (formData);
            }

            var addUserDto = new AddUserDto()
            {
                FirstName = formData.FirstName.Trim(),
                LastName = formData.LastName.Trim(),
                Email = formData.Email.Trim(),
                Password = formData.Password.Trim(),
            };
           var result =  _userService.AddUser(addUserDto);

            if (result.IsSucceed)
            {
                ViewBag.Errormessage = result.Message;
                return View(formData); //işlem başarılı ise mesajı görüntüle, değilse verileri dolu gönder.
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
                return View(formData);
            }
        }

        public async Task<IActionResult> Login(LoginViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home"); //giriş yapılırken hata yapılırsa tekrar ana sayfaya geri gönder.
            }

            var loginDto = new LoginDto()
            {
                Email = formData.Email,
                Password = formData.Password,
            };
            var userInfo = _userService.LoginUser(loginDto);
            if (userInfo is null)
            {
                TempData["ErrorMessage"] = "Kullanıcı adı veya Şifre hatalı.";
                return RedirectToAction("Index", "Home");
            }

            var claims = new List<Claim>();
            claims.Add(new Claim("id", userInfo.Id.ToString()));
            claims.Add(new Claim("email", userInfo.Email));
            claims.Add(new Claim("firstName", userInfo.FirstName));
            claims.Add(new Claim("lastName", userInfo.LastName));
            claims.Add(new Claim("userType", userInfo.UserType.ToString()));

            //yekilendirme özel claim

            claims.Add(new Claim(ClaimTypes.Role, userInfo.UserType.ToString()));
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            // claims listesindeki verilerle bir oturum açılacağı için yukarıdaki identity nesnesini tanımladım.

            var autProperties = new AuthenticationProperties
            {
                AllowRefresh = true, // yenilenebilir oturum.
                ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(6)) // oturum açıldıktan sonra 6 saat geçerli.
            };


            // await asenkronize (eşzamansız) yapıların birbirlerini bekleyerek çalışmalırını sağlıyor.
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);

            TempData["LoginMessage"] = "Giriş başarıyla yapıldı.";

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout() //asenkron olduğundan Task yapıldı
        {
            await HttpContext.SignOutAsync(); // oturumu kapat.

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Update()
        {
            return View();
        }
    }
 }


