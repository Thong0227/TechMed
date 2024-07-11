using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.Banner;
using TechMed.Areas.Admin.Models.News;
using TechMed.Areas.Admin.Models.Recruitment;
using X.PagedList;

namespace TechMed.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;

        public NewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/News
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10; //SL phan tu tren 1 trang
            int pageNumber = (page ?? 1);
            var news = await _context.News
              .Include(n => n.NewsCategory)
              .OrderByDescending(n => n.CreatedDate)
              .ToListAsync();
            IPagedList<News> newsPaging = news.ToPagedList(pageNumber, pageSize);

            if (news != null)
            {
                return View(newsPaging);
            }
            else {
                return Problem("Dữ liệu không hợp lệ");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string? keyword, int? page)
        {
            int pageSize = 5; //SL phan tu tren 1 trang
            int pageNumber = (page ?? 1);
            IQueryable<News> newsQuery = _context.News.Include(b => b.NewsCategory).OrderByDescending(n => n.CreatedDate);
            if (!string.IsNullOrEmpty(keyword))
            {
                newsQuery = newsQuery.Where(n => n.Title.Contains(keyword));
            }
            var news = await newsQuery.ToListAsync();
            IPagedList<News> newsPaging = news.ToPagedList(pageNumber, pageSize);

            ViewBag.keyword = keyword;
            if (news != null)
            {
                return View(newsPaging);
            }
            else
            {
                return Problem("Entity set 'AppDbContext.News' is null.");
            }
        }

        // GET: Admin/News/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create
        public IActionResult Create()
        {
            ViewData["NewsCategoryId"] = new SelectList(_context.NewsCategory, "Id", "Name");
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
                [Bind("Id,Title,Description,Content,Status,Image,Order,Type,CreatedDate,StartDate,EndDate,NewsCategoryId")] News news, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    // Tạo tên tệp ảnh duy nhất
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;

                    // Lưu ảnh vào thư mục của ứng dụng
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/News", uniqueFileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    news.Image = Url.Content(Path.Combine("~/Upload/News/", uniqueFileName));
                }
                news.CreatedDate = DateTime.Now;
                news.Id = Guid.NewGuid();
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NewsCategoryId"] = new SelectList(_context.NewsCategory, "Id", "Name", news.NewsCategoryId);
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["NewsCategoryId"] = new SelectList(_context.NewsCategory, "Id", "Name", news.NewsCategoryId);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,Content,Status,Image,Order,Type,CreatedDate,StartDate,EndDate,NewsCategoryId")] News news,
            IFormFile? Image, string? imgData)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null && Image.Length > 0)
                    {
                        if (imgData != null)
                        {
                            DeleteNewsImage(imgData);
                        }
                        // Tạo tên tệp ảnh duy nhất
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                        // Lưu ảnh vào thư mục của ứng dụng
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/News", uniqueFileName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream);
                        }

                        news.Image = Url.Content(Path.Combine("~/Upload/News/", uniqueFileName));
                    }
                    else
                    {
                        news.Image = imgData;

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
            ViewData["NewsCategoryId"] = new SelectList(_context.NewsCategory, "Id", "Name", news.NewsCategoryId);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.News == null)
            {
                return Problem("Dữ liệu không hợp lệ");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
                if (news.Image != null)
                {
                    DeleteNewsImage(news.Image);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(Guid id)
        {
            return (_context.News?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //Hàm xóa ảnh
        private void DeleteNewsImage(string ImageUrl)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/News", ImageUrl.Replace("/Upload/News/", ""));
            // Kiểm tra xem tệp ảnh tồn tại không
            if (System.IO.File.Exists(imagePath))
            {
                // Nếu tệp ảnh tồn tại, xóa nó khỏi thư mục
                System.IO.File.Delete(imagePath);
            }
        }
	}
}
