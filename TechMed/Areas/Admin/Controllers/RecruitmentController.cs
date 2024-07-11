using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.Banner;
using TechMed.Areas.Admin.Models.News;
using TechMed.Areas.Admin.Models.Recruitment;
using X.PagedList;


namespace TechMed.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RecruitmentController : Controller
    {
        private readonly AppDbContext _context;

        public RecruitmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Recruitment
        public async Task<IActionResult> Index(int? page)
        {
            if (_context.Recruitments != null)
            {
                int pageSize = 10; //SL phan tu tren 1 trang
                int pageNumber = (page ?? 1);
                var recruitments = await _context.Recruitments
                            .Include(r => r.RecruitmentCate) // Load thông tin của danh mục
                            .Include(r => r.RecruitmentTags) // Load thông tin của các tag
                                .ThenInclude(rt => rt.Tag) // Load thông tin của tag từ RecruitmentTag
								.OrderByDescending(n => n.CreatedDate)
							.ToListAsync();
                IPagedList<Recruitment> recruitmentsPaging = recruitments.ToPagedList(pageNumber, pageSize);
                if (recruitments != null)
                {
                    return View(recruitmentsPaging);
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string? keyword, int? page)
        {
            if (_context.Recruitments != null)
            {
                int pageSize = 10; //SL phan tu tren 1 trang
                int pageNumber = (page ?? 1);
                IQueryable<Recruitment> recruitmentQuery = _context.Recruitments.Include(r => r.RecruitmentCate)
                    .Include(r => r.RecruitmentTags).ThenInclude(rt => rt.Tag).OrderByDescending(n => n.CreatedDate);

                if (!string.IsNullOrEmpty(keyword))
                {
                    recruitmentQuery = recruitmentQuery.Where(n => n.Name.Contains(keyword));
                }
                var recruitments = await recruitmentQuery.ToListAsync();
                IPagedList<Recruitment> recruitmentsPaging = recruitments.ToPagedList(pageNumber, pageSize);

                ViewBag.keyword = keyword;

                if (recruitments != null)
                {
                    return View(recruitmentsPaging);
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Recruitment/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (_context.Recruitments != null)
            {
                var recruitment = await _context.Recruitments
                .Include(r => r.RecruitmentCate)
                .Include(r => r.RecruitmentTags).ThenInclude(r => r.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
                if (recruitment == null)
                {
                    return NotFound();
                }

                return View(recruitment);
            }
            return View();
        }

        // GET: Recruitment/Create
        public IActionResult Create()
        {
            ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name");
            ViewBag.RecruitmentCates = new SelectList(_context.RecruitmentCate, "Id", "Name");
            return View();
        }

        // POST: Recruitment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recruitment recruitment, List<Guid> RecruitmentTags, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    // Tạo tên tệp ảnh duy nhất
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                    // Lưu ảnh vào thư mục của ứng dụng
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Recruitment", uniqueFileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    recruitment.Image = Url.Content(Path.Combine("~/Upload/Recruitment/", uniqueFileName));
                }
                recruitment.Id = Guid.NewGuid();

                if (RecruitmentTags != null && RecruitmentTags.Count > 0)
                {
                    recruitment.RecruitmentTags = RecruitmentTags.Select(tagId => new RecruitmentTag { RecruitmentId = recruitment.Id, TagId = tagId }).ToList();
                }

                _context.Add(recruitment);

                // Lưu danh sách RecruitmentTag vào cơ sở dữ liệu
                if (recruitment.RecruitmentTags != null && recruitment.RecruitmentTags.Count > 0)
                {
                    foreach (var recruitmentTag in recruitment.RecruitmentTags)
                    {
                        _context.Add(recruitmentTag);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState.IsValid trả về false, quay lại trang tạo mới và hiển thị lại form với dữ liệu đã nhập
            ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name", RecruitmentTags);
            ViewBag.RecruitmentCates = new SelectList(_context.RecruitmentCate, "Id", "Name", recruitment.RecruitmentCateId);
            return View(recruitment);
        }

        // GET: Recruitment/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (_context.Recruitments != null)
            {
                var recruitment = await _context.Recruitments.Include(r => r.RecruitmentTags).FirstOrDefaultAsync(r => r.Id == id);
                if (recruitment == null)
                {
                    return NotFound();
                }
                ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name", selectedValues: recruitment.RecruitmentTags.Select(rt => rt.TagId));
                ViewBag.RecruitmentCates = new SelectList(_context.RecruitmentCate, "Id", "Name", recruitment.RecruitmentCateId);
                return View(recruitment);
            }
            return View();
        }

        // POST: Recruitment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Recruitment recruitment, List<Guid> Tags, IFormFile? Image, string? imgData)
        {
            if (id != recruitment.Id)
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
                            DeleteImage(imgData);
                        }
                        // Tạo tên tệp ảnh duy nhất
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                        // Lưu ảnh vào thư mục của ứng dụng
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Recruitment", uniqueFileName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream);
                        }

                        recruitment.Image = Url.Content(Path.Combine("~/Upload/Recruitment/", uniqueFileName));
                    }
                    else
                    {
                        recruitment.Image = imgData;

                    }
                    // Xóa tất cả RecruitmentTag cũ liên quan đến recruitment.Id
                    var existingTags = _context.RecruitmentTags.Where(rt => rt.RecruitmentId == recruitment.Id);
                    _context.RecruitmentTags.RemoveRange(existingTags);

                    if (Tags != null && Tags.Count > 0)
                    {
                        recruitment.RecruitmentTags = Tags.Select(tagId => new RecruitmentTag { RecruitmentId = recruitment.Id, TagId = tagId }).ToList();
                    }

                    // Lưu danh sách RecruitmentTag vào cơ sở dữ liệu
                    if (recruitment.RecruitmentTags != null && recruitment.RecruitmentTags.Count > 0)
                    {
                        foreach (var recruitmentTag in recruitment.RecruitmentTags)
                        {
                            // Thêm mới recruitmentTag vào DbContext
                            _context.Add(recruitmentTag);
                        }
                    }
                    _context.Update(recruitment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecruitmentExists(recruitment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name", Tags);
            ViewBag.RecruitmentCates = new SelectList(_context.RecruitmentCate, "Id", "Name", recruitment.RecruitmentCateId);
            return View(recruitment);
        }

        // GET: Recruitment/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (_context.Recruitments != null)
            {
                var recruitment = await _context.Recruitments
                .Include(r => r.RecruitmentTags)
                .ThenInclude(rt => rt.Tag)
            .FirstOrDefaultAsync(m => m.Id == id);
                if (recruitment == null)
                {
                    return NotFound();
                }

                return View(recruitment);
            }
            return View();
        }

        // POST: Recruitment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var recruitment = await _context.Recruitments
                .Include(r => r.RecruitmentTags)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recruitment == null)
            {
                return NotFound();
            }

            // Xóa các RecruitmentTag liên quan
            _context.RecruitmentTags.RemoveRange(recruitment.RecruitmentTags);

            // Xóa Recruitment
            _context.Recruitments.Remove(recruitment);
            if (recruitment.Image != null)
            {
                DeleteImage(recruitment.Image);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool RecruitmentExists(Guid id)
        {
            return _context.Recruitments.Any(e => e.Id == id);
        }

        //Hàm xóa ảnh
        private void DeleteImage(string bannerUrl)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Recruitment", bannerUrl.Replace("/Upload/Recruitment/", ""));
            // Kiểm tra xem tệp ảnh tồn tại không
            if (System.IO.File.Exists(imagePath))
            {
                // Nếu tệp ảnh tồn tại, xóa nó khỏi thư mục
                System.IO.File.Delete(imagePath);
            }
        }
    }
}

