using DevXuongMoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevXuongMoc.Controllers
{
    public class NewController : Controller
    {
        private readonly XuongMocContext _context;
        public NewController(XuongMocContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _context.News.DefaultIfEmpty().ToListAsync();

            return View(data);
        }
        public async Task<IActionResult> New()
        {
            var data = await _context.News.DefaultIfEmpty().ToListAsync();

            return View(data);
        }
    }
}