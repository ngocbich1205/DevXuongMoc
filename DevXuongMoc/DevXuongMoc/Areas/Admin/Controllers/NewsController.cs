using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevXuongMoc.Models;
using X.PagedList;

namespace DevXuongMoc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly XuongMocContext _context;

        public NewsController(XuongMocContext context)
        {
            _context = context;
        }

        // GET: Admin/News
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 4;
            //var category = await _context.Categories.ToListAsync();
            var news = await _context.News.OrderBy(c => c.Id).ToPagedListAsync(page, limit);
            // nếu có tham số name trên url
            if (!String.IsNullOrEmpty(name))
            {
                news = await _context.News.Where(c => c.Title.Contains(name)).OrderBy(c => c.Id).ToPagedListAsync(page, limit);
            }
            ViewBag.keyword = name;
            return View(news);
        }

        // GET: Admin/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }
            // Return partial view for AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Details", news);
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public IActionResult Create()
        {
            // Return partial view for AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Create");
            }
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Title,Description,Content,Image,MetaTitle,MainKeyword,MetaKeyword,MetaDescription,Slug,Views,Likes,Star,CreatedDate,UpdatedDate,AdminCreated,AdminUpdated,Status,Isdelete")] News news)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Any() && files[0].Length > 0)
                {
                    var file = files[0];
                    var fileName = file.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\news", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        news.Image = "/images/news/" + fileName;
                    }
                }
                _context.Add(news);
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
                return PartialView("_Create", news);
            }
            return View(news);

        }

        // GET: Admin/News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            // Return partial view for AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Edit", news);
            }
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Title,Description,Content,Image,MetaTitle,MainKeyword,MetaKeyword,MetaDescription,Slug,Views,Likes,Star,CreatedDate,UpdatedDate,AdminCreated,AdminUpdated,Status,Isdelete")] News news)
        {
            if (id != news.Id)
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
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\news", FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            news.Image = "/images/news/" + FileName; //gán tên ảnh cho thuộc tính Image
                        }
                    }
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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
            return View(news);
        }

        // GET: Admin/News/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var news = await _context.News
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (news == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(news);
        //}

        //// POST: Admin/News/Delete/5
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    var user = _context.News.FirstOrDefault(u => u.Id == id);
        //    if (user == null)
        //    {
        //        // Nếu không tìm thấy người dùng, redirect đến danh sách hoặc hiển thị lỗi
        //        return NotFound();
        //    }

        //    // Tiến hành xóa người dùng
        //    _context.News.Remove(user);
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

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            // Return partial view for AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Delete", news);
            }
            return View(news);
        }

        // POST: AdminQL/AdminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
