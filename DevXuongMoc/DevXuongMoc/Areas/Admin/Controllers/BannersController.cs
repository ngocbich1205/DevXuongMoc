using DevXuongMoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevXuongMoc.Models;
using X.PagedList;

namespace DevXuongMoc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannersController : Controller
    {
        private readonly XuongMocContext _context;

        public BannersController(XuongMocContext context)
        {
            _context = context;
        }

        // GET: Admin/Banners
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 4;
            //var category = await _context.Categories.ToListAsync();
            var banner = await _context.Banners.OrderBy(c => c.Id).ToPagedListAsync(page, limit);
            // nếu có tham số name trên url
            if (!String.IsNullOrEmpty(name))
            {
                banner = await _context.Banners.Where(c => c.Title.Contains(name)).OrderBy(c => c.Id).ToPagedListAsync(page, limit);
            }
            ViewBag.keyword = name;
            return View(banner);
        }

        // GET: Admin/Banners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }
            // Return partial view for AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Details", banner);
            }
            return View(banner);
        }

        // GET: Admin/Banners/Create
        public IActionResult Create()
        {
            // Return partial view for AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Create");
            }
            return View();
        }

        // POST: Admin/Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Title,SubTitle,Urls,Orders,Type,CreatedDate,UpdatedDate,AdminCreated,AdminUpdated,Notes,Status,Isdelete")] Banner banner)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Any() && files[0].Length > 0)
                {
                    var file = files[0];
                    var fileName = file.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\anhcat", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        banner.Image = "/images/anhcat/" + fileName;
                    }
                }
                _context.Add(banner);
                await _context.SaveChangesAsync();

                // Return success response for AJAX
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, redirectUrl = Url.Action("Index") });
                }

                return RedirectToAction(nameof(Index));
            }

            // Return partial view for AJAX in case of validation errors
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Create", banner);
            }
            return View(banner);

        }

        // GET: Admin/Banners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners.FindAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            // Return partial view for AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Edit", banner);
            }
            return View(banner);
        }

        // POST: Admin/Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Title,SubTitle,Urls,Orders,Type,CreatedDate,UpdatedDate,AdminCreated,AdminUpdated,Notes,Status,Isdelete")] Banner banner)
        {
            if (id != banner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count() > 0 && files[0].Length > 0)
                    {
                        var file = files[0];
                        var FileName = file.FileName;
                        // upload ảnh vào thư mục wwwroot\\images\\Category 
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\anhcat", FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            banner.Image = "/images/anhcat/" + FileName; //gán tên ảnh cho thuộc tính Image
                        }
                    }
                    _context.Update(banner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerExists(banner.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(banner);
        }

        // GET: Admin/Banners/Delete/5


        //// POST: Admin/Banners/Delete/5
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    var banner = _context.Banners.FirstOrDefault(u => u.Id == id);
        //    if (banner == null)
        //    {
        //        // Nếu không tìm thấy người dùng, redirect đến danh sách hoặc hiển thị lỗi
        //        return NotFound();
        //    }

        //    // Tiến hành xóa người dùng
        //    _context.Banners.Remove(banner);
        //    _context.SaveChanges();

        //    // Redirect về trang danh sách người dùng hoặc trang khác sau khi xóa thành công
        //    return RedirectToAction(nameof(Index));  // Giả sử bạn sẽ chuyển hướng về trang danh sách người dùng
        //}
        // GET: AdminQL/Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            // Return partial view for AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Delete", banner);
            }
            return View(banner);
        }

        // POST: AdminQL/AdminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner != null)
            {
                _context.Banners.Remove(banner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool BannerExists(int id)
        {
            return _context.Banners.Any(e => e.Id == id);
        }
    }
}
