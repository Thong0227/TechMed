using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.News;
using TechMed.Areas.Admin.Models.Recruitment;
using X.PagedList;

namespace TechMed.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RecruitmentCatesController : Controller
    {
        private readonly AppDbContext _context;

        public RecruitmentCatesController(AppDbContext context)
        {
            _context = context;
        }

		// GET: Admin/RecruitmentCates
		public async Task<IActionResult> Index(int? page)
		{
			int pageSize = 10; //SL phan tu tren 1 trang
			int pageNumber = (page ?? 1);
			if (_context.RecruitmentCate == null)
			{
				return Problem("Entity set 'AppDbContext.Banner' is null.");
			}
			var recruitmentCates = await _context.RecruitmentCate.ToListAsync();
			IPagedList<RecruitmentCate> recruitmentCatesPaging = recruitmentCates.ToPagedList(pageNumber, pageSize);
			if (recruitmentCates != null)
			{
				return View(recruitmentCatesPaging);
			}
			else
			{
				return Problem("Entity set 'AppDbContext.Banner' is null.");
			}
		}


		[HttpPost]
		public async Task<IActionResult> Index(string? keyword, int? page)
		{
			int pageSize = 10; //SL phan tu tren 1 trang
			int pageNumber = (page ?? 1);
			if (_context.RecruitmentCate == null)
			{
				return Problem("Entity set 'AppDbContext.Banner' is null.");
			}
			IQueryable<RecruitmentCate> recruitmentCatesQuerry = _context.RecruitmentCate;

			if (!string.IsNullOrEmpty(keyword))
			{
				recruitmentCatesQuerry = recruitmentCatesQuerry.Where(n => n.Name.Contains(keyword));
			}
			var recruitmentCates = await recruitmentCatesQuerry.ToListAsync();

			IPagedList<RecruitmentCate> recruitmentCatesPaging = recruitmentCates.ToPagedList(pageNumber, pageSize);
			ViewBag.keyword = keyword;
			if (recruitmentCates != null)
			{
				return View(recruitmentCatesPaging);
			}
			else
			{
				return Problem("Entity set 'AppDbContext.Banner' is null.");
			}
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
