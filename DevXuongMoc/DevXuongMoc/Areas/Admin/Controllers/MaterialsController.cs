using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevXuongMoc.Models;
using X.PagedList;
using System.Reflection;

namespace DevXuongMoc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MaterialsController : Controller
    {
        private readonly XuongMocContext _context;

        public MaterialsController(XuongMocContext context)
        {
            _context = context;
        }

        // GET: Admin/Materials
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 4;
            //var category = await _context.Categories.ToListAsync();
            var material = await _context.Materials.OrderBy(c => c.Id).ToPagedListAsync(page, limit);
            // nếu có tham số name trên url
            if (!String.IsNullOrEmpty(name))
            {
                material = await _context.Materials.Where(c => c.Title.Contains(name)).OrderBy(c => c.Id).ToPagedListAsync(page, limit);
            }
            ViewBag.keyword = name;
            return View(material);
        }

        // GET: Admin/Materials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Materials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }
            // Return partial view for AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Details", material);
            }
            return View(material);
        }

        // GET: Admin/Materials/Create
        public IActionResult Create()
        {
            // Return partial view for AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Create");
            }
            return View();
        }

        // POST: Admin/Materials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Icon,MetaTitle,MetaKeyword,MetaDescription,Slug,Orders,Parentid,CreatedDate,UpdatedDate,AdminCreated,AdminUpdated,Notes,Status,Isdelete")] Material material)
        {
            if (ModelState.IsValid)
            {
                _context.Add(material);
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
                return PartialView("_Create", material);
            }
            return View(material);
        }

        // GET: Admin/Materials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            // Return partial view for AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Edit", material);
            }
            return View(material);
        }

        // POST: Admin/Materials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Icon,MetaTitle,MetaKeyword,MetaDescription,Slug,Orders,Parentid,CreatedDate,UpdatedDate,AdminCreated,AdminUpdated,Notes,Status,Isdelete")] Material material)
        {
            if (id != material.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(material.Id))
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
            return View(material);
        }

        // GET: Admin/Materials/Delete/5


        //// POST: Admin/Materials/Delete/5
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    var user = _context.Materials.FirstOrDefault(u => u.Id == id);
        //    if (user == null)
        //    {
        //        // Nếu không tìm thấy người dùng, redirect đến danh sách hoặc hiển thị lỗi
        //        return NotFound();
        //    }

        //    // Tiến hành xóa người dùng
        //    _context.Materials.Remove(user);
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

            var material = await _context.Materials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }

            // Return partial view for AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Delete", material);
            }
            return View(material);
        }

        // POST: AdminQL/AdminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material != null)
            {
                _context.Materials.Remove(material);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool MaterialExists(int id)
        {
            return _context.Materials.Any(e => e.Id == id);
        }
    }
}
