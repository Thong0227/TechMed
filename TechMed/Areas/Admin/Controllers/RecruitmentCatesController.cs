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
    public class RecruitmentCatesController : Controller
    {
        private readonly AppDbContext _context;

        public RecruitmentCatesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/RecruitmentCates
        public async Task<IActionResult> Index()
        {
              return _context.RecruitmentCate != null ? 
                          View(await _context.RecruitmentCate.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.RecruitmentCate'  is null.");
        }

        // GET: Admin/RecruitmentCates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.RecruitmentCate == null)
            {
                return NotFound();
            }

            var recruitmentCate = await _context.RecruitmentCate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recruitmentCate == null)
            {
                return NotFound();
            }

            return View(recruitmentCate);
        }

        // GET: Admin/RecruitmentCates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/RecruitmentCates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] RecruitmentCate recruitmentCate)
        {
            if (ModelState.IsValid)
            {
                recruitmentCate.Id = Guid.NewGuid();
                _context.Add(recruitmentCate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recruitmentCate);
        }

        // GET: Admin/RecruitmentCates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.RecruitmentCate == null)
            {
                return NotFound();
            }

            var recruitmentCate = await _context.RecruitmentCate.FindAsync(id);
            if (recruitmentCate == null)
            {
                return NotFound();
            }
            return View(recruitmentCate);
        }

        // POST: Admin/RecruitmentCates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] RecruitmentCate recruitmentCate)
        {
            if (id != recruitmentCate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recruitmentCate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecruitmentCateExists(recruitmentCate.Id))
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
            return View(recruitmentCate);
        }

        // GET: Admin/RecruitmentCates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.RecruitmentCate == null)
            {
                return NotFound();
            }

            var recruitmentCate = await _context.RecruitmentCate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recruitmentCate == null)
            {
                return NotFound();
            }

            return View(recruitmentCate);
        }

        // POST: Admin/RecruitmentCates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.RecruitmentCate == null)
            {
                return Problem("Entity set 'AppDbContext.RecruitmentCate'  is null.");
            }
            var recruitmentCate = await _context.RecruitmentCate.FindAsync(id);
            if (recruitmentCate != null)
            {
                _context.RecruitmentCate.Remove(recruitmentCate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecruitmentCateExists(Guid id)
        {
          return (_context.RecruitmentCate?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
