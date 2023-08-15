using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] //admin istek atılması için Cs ile beraber çalışıyor.
    [Authorize(Roles ="Admin")] // hangi rollerin gelmesini istiyorsak buradan Claims ile rol veriyoruz.
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
