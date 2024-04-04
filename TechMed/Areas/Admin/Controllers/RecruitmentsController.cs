using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.Recruitment;

namespace TechMed.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RecruitmentsController : Controller
    {
        private readonly AppDbContext _context;

        public RecruitmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Recruitments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Recruitment.Include(r => r.RecruitmentCate);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Recruitments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Recruitment == null)
            {
                return NotFound();
            }

            var recruitment = await _context.Recruitment
                .Include(r => r.RecruitmentCate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recruitment == null)
            {
                return NotFound();
            }

            return View(recruitment);
        }

        // GET: Admin/Recruitments/Create
        public IActionResult Create()
        {
            ViewData["RecruitmentCateId"] = new SelectList(_context.RecruitmentCate, "Id", "Name");
            if(_context.Tag != null)
            {
                ViewBag.Tags = _context.Tag.ToList();
            }
            else
            {
                ViewBag.Tags = null;
            }
            return View();
        }

        // POST: Admin/Recruitments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Salary,Description,Content,Deadline,Quantity,RecruitmentCateId")] Recruitment recruitment,
            List<Guid> selectedTags
        )
        {
            if (ModelState.IsValid)
            {
                recruitment.Id = Guid.NewGuid();
                if (selectedTags != null)
                {
                    foreach (var tagId in selectedTags)
                    {
                        recruitment.RecruitmentTags.Add(new RecruitmentTag
                        {
                            RecruitmentId = recruitment.Id,
                            TagId = tagId
                        });
                    }
                }
                _context.Add(recruitment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecruitmentCateId"] = new SelectList(_context.RecruitmentCate, "Id", "Name", recruitment.RecruitmentCateId);
            if (_context.Tag != null)
            {
                ViewBag.Tags = _context.Tag.ToList();
            }
            else
            {
                ViewBag.Tags = null;
            }
            return View(recruitment);
        }

        // GET: Admin/Recruitments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Recruitment == null)
            {
                return NotFound();
            }

            var recruitment = await _context.Recruitment.Include(r => r.RecruitmentTags).FirstOrDefaultAsync(r => r.Id == id);
            if (recruitment == null)
            {
                return NotFound();
            }
            if(_context.Tag != null)
            {
                ViewBag.Tags = _context.Tag.ToList();
            }
            var selectedTags = recruitment.RecruitmentTags.Select(rt => rt.TagId).ToList();
            recruitment.SelectedTags = selectedTags;
            ViewData["RecruitmentCateId"] = new SelectList(_context.RecruitmentCate, "Id", "Name", recruitment.RecruitmentCateId);
            return View(recruitment);
        }

        // POST: Admin/Recruitments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, 
            [Bind("Id,Name,Salary,Description,Content,Deadline,Quantity,RecruitmentCateId")] Recruitment recruitment,
            List<Guid> selectedTags
        )
        {
            if (id != recruitment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(_context.RecruitmentTag != null)
                    {
                        // Remove existing tags for the recruitment
                        _context.RecruitmentTag.RemoveRange(_context.RecruitmentTag.Where(rt => rt.RecruitmentId == recruitment.Id));
                    }

                    // Add selected tags to the recruitment
                    if (selectedTags != null)
                    {
                        foreach (var tagId in selectedTags)
                        {
                            recruitment.RecruitmentTags.Add(new RecruitmentTag
                            {
                                RecruitmentId = recruitment.Id,
                                TagId = tagId
                            });
                        }
                    }

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
            ViewData["RecruitmentCateId"] = new SelectList(_context.RecruitmentCate, "Id", "Name", recruitment.RecruitmentCateId);
            return View(recruitment);
        }

        // GET: Admin/Recruitments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Recruitment == null)
            {
                return NotFound();
            }

            var recruitment = await _context.Recruitment
                .Include(r => r.RecruitmentCate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recruitment == null)
            {
                return NotFound();
            }

            return View(recruitment);
        }

        // POST: Admin/Recruitments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Recruitment == null)
            {
                return Problem("Entity set 'AppDbContext.Recruitment'  is null.");
            }
            var recruitment = await _context.Recruitment.FindAsync(id);
            if (recruitment != null)
            {
                _context.Recruitment.Remove(recruitment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecruitmentExists(Guid id)
        {
          return (_context.Recruitment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
