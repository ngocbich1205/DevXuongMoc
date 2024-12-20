using DevXuongMoc.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DevXuongMoc.Areas.Admins.Controllers
{
    //[Area("Admin")]
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
