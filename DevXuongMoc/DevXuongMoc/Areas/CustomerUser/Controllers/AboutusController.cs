using Microsoft.AspNetCore.Mvc;

namespace DevXuongMoc.Areas.CustomerUser.Controllers
{
    [Area("CustomerUser")]
    public class AboutusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
