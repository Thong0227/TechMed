using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.Recruitment;


namespace TechMed.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RecruitmentController : Controller
    {
        private readonly AppDbContext _context;

        public RecruitmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Recruitment
        public async Task<IActionResult> Index()
        {
            if (_context.Recruitments != null)
            {
                var recruitments = await _context.Recruitments.ToListAsync();
                return View(recruitments);
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
        public async Task<IActionResult> Create(Recruitment recruitment, List<Guid> selectedTags)
        {
            if (ModelState.IsValid)
            {
                recruitment.Id = Guid.NewGuid();

                if (selectedTags != null)
                {
                    recruitment.RecruitmentTags = selectedTags.Select(tagId => new RecruitmentTag { RecruitmentId = recruitment.Id, TagId = tagId }).ToList();
                }

                _context.Add(recruitment);

                // Lưu danh sách RecruitmentTag vào cơ sở dữ liệu
                if(recruitment.RecruitmentTags != null)
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
            ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name", selectedTags);
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
                ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name", recruitment.RecruitmentTags.Select(rt => rt.TagId));
                ViewBag.RecruitmentCates = new SelectList(_context.RecruitmentCate, "Id", "Name", recruitment.RecruitmentCateId);
                return View(recruitment);
            }
            return View();
        }

        // POST: Recruitment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Recruitment recruitment, List<Guid> selectedTags)
        {
            if (id != recruitment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recruitment);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name", selectedTags);
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
            var recruitment = await _context.Recruitments.FindAsync(id);
            _context.Recruitments.Remove(recruitment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecruitmentExists(Guid id)
        {
            return _context.Recruitments.Any(e => e.Id == id);
        }
    }
}
    
