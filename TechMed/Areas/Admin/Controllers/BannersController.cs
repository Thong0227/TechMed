using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.Banner;
using X.PagedList;

namespace TechMed.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BannersController : Controller
    {
        private readonly AppDbContext _context;

		public BannersController(AppDbContext context)
        {
            _context = context;
		}

        // GET: Admin/Banners
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10; //SL phan tu tren 1 trang
            int pageNumber = (page ?? 1);
			var banners = await _context.Banner
						  .Include(b => b.BannerPosition)
						  .ToListAsync();
            IPagedList<Banner> bannerPaging = banners.ToPagedList(pageNumber, pageSize);
            if (banners != null)
			{
				return View(bannerPaging);
			}
			else
			{
				// Nếu danh sách Banner là null, trả về thông báo lỗi
				return Problem("Entity set 'AppDbContext.Banner' is null.");
			}
		}

        [HttpPost]
		// GET: Admin/Banners
		public async Task<IActionResult> Index(string? keyword, int? page)
		{
			int pageSize = 10; //Số phần tử trên mỗi trang
			int pageNumber = page ?? 1;

			IQueryable<Banner> bannerQuery = _context.Banner.Include(b => b.BannerPosition);

			if (!string.IsNullOrEmpty(keyword))
			{
				bannerQuery = bannerQuery.Where(n => n.Name.Contains(keyword));
			}

			var banners = await bannerQuery.ToListAsync();
			IPagedList<Banner> bannerPaging = banners.ToPagedList(pageNumber, pageSize);

			ViewBag.keyword = keyword;

			if (banners != null)
			{
				return View(bannerPaging);
			}
			else
			{
				// Nếu danh sách Banner là null, trả về thông báo lỗi
				return Problem("Entity set 'AppDbContext.Banner' is null.");
			}
		}

		// GET: Admin/Banners/Details/5
		public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Banner == null)
            {
                return NotFound();
            }

            var banner = await _context.Banner
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // GET: Admin/Banners/Create
        public IActionResult Create()
        {
            ViewData["BannerPositionId"] = new SelectList(_context.BannerPosition, "Id", "Name");
            return View();
        }

        // POST: Admin/Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,ImageUrl,Status,Order,Link,StartDate,EndDate,BannerPositionId")] Banner banner, IFormFile? ImageUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImageUrl != null && ImageUrl.Length > 0)
                {
                    // Tạo tên tệp ảnh duy nhất
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageUrl.FileName;

					// Lưu ảnh vào thư mục của ứng dụng
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/banner", uniqueFileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(stream);
                    }

                    banner.ImageUrl = Url.Content(Path.Combine("~/Upload/banner/", uniqueFileName));
                }
                banner.Id = Guid.NewGuid();
                _context.Add(banner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BannerPositionId"] = new SelectList(_context.BannerPosition, "Id", "Name",banner.BannerPositionId);
            return View(banner);
        }

        // GET: Admin/Banners/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Banner == null)
            {
                return NotFound();
            }

            var banner = await _context.Banner.FindAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            ViewData["BannerPositionId"] = new SelectList(_context.BannerPosition, "Id", "Name", banner.BannerPositionId);
            return View(banner);
        }

        // POST: Admin/Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, 
            [Bind("Id,Name,ImageUrl,Status,Order,Link,StartDate,EndDate,BannerPositionId")] Banner banner, IFormFile? ImageUrl, string? imgData)
        {
            if (id != banner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageUrl != null && ImageUrl.Length > 0)
                    {
						if (imgData != null)
						{
							DeleteBannerImage(imgData);
						}
						// Tạo tên tệp ảnh duy nhất
						var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageUrl.FileName;

                        // Lưu ảnh vào thư mục của ứng dụng
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/banner", uniqueFileName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await ImageUrl.CopyToAsync(stream);
                        }

                        banner.ImageUrl = Url.Content(Path.Combine("~/Upload/banner/", uniqueFileName));
                    }
                    else
                    {
                        banner.ImageUrl = imgData;

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
            ViewData["BannerPositionId"] = new SelectList(_context.BannerPosition, "Id", "Name", banner.BannerPositionId);
            return View(banner);
        }

        // GET: Admin/Banners/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Banner == null)
            {
                return NotFound();
            }

            var banner = await _context.Banner
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // POST: Admin/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
            {
            if (_context.Banner == null)
            {
                return Problem("Dữ liệu không tồn tại");
            }
            var banner = await _context.Banner.FindAsync(id);
            if (banner != null)
            {
				_context.Banner.Remove(banner);
                if (banner.ImageUrl != null)
                {
                    DeleteBannerImage(banner.ImageUrl);
				}
			}
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerExists(Guid id)
        {
          return (_context.Banner?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        //Hàm xóa ảnh
        private void DeleteBannerImage(string bannerUrl)
        {
			var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/banner", bannerUrl.Replace("/Upload/banner/", ""));
			// Kiểm tra xem tệp ảnh tồn tại không
			if (System.IO.File.Exists(imagePath))
			{
				// Nếu tệp ảnh tồn tại, xóa nó khỏi thư mục
				System.IO.File.Delete(imagePath);
			}
		}
    }
}
