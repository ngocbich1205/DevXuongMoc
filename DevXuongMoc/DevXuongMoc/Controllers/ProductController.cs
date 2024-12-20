using DevXuongMoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DevXuongMoc.Controllers
{
    public class ProductController : Controller
    {
        public readonly XuongMocContext _context;
        public ProductController(XuongMocContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 8;
            //var category = await _context.Categories.ToListAsync();
            var product = await _context.Products.OrderBy(c => c.Id).ToPagedListAsync(page, limit);
            // nếu có tham số name trên url
            if (!String.IsNullOrEmpty(name))
            {
                product = await _context.Products.Where(c => c.Title.Contains(name)).OrderBy(c => c.Id).ToPagedListAsync(page, limit);
            }
            ViewBag.keyword = name;
            return View(product);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.DefaultIfEmpty()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
