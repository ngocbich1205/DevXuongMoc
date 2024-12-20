using DevXuongMoc.Controllers;
using DevXuongMoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevXuongMoc.Areas.CustomerUser.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly XuongMocContext _context;

        public DashboardController(ILogger<DashboardController> logger, XuongMocContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index(int? id)
        {
            var products = _context.Products.Take(4).ToList();
            return View(products);
        }
    }


}