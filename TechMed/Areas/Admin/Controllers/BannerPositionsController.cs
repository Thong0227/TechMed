using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.Banner;

namespace TechMed.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerPositionsController : Controller
    {
        private readonly AppDbContext _context;

        public BannerPositionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/BannerPositions
        public async Task<IActionResult> Index()
        {
              return _context.BannerPosition != null ? 
                          View(await _context.BannerPosition.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.BannerPosition'  is null.");
        }

        // GET: Admin/BannerPositions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.BannerPosition == null)
            {
                return NotFound();
            }

            var bannerPosition = await _context.BannerPosition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bannerPosition == null)
            {
                return NotFound();
            }

            return View(bannerPosition);
        }

        // GET: Admin/BannerPositions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/BannerPositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,Description")] BannerPosition bannerPosition)
        {
            if (ModelState.IsValid)
            {
                bannerPosition.Id = Guid.NewGuid();
                _context.Add(bannerPosition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bannerPosition);
        }

        // GET: Admin/BannerPositions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.BannerPosition == null)
            {
                return NotFound();
            }

            var bannerPosition = await _context.BannerPosition.FindAsync(id);
            if (bannerPosition == null)
            {
                return NotFound();
            }
            return View(bannerPosition);
        }

        // POST: Admin/BannerPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Code,Description")] BannerPosition bannerPosition)
        {
            if (id != bannerPosition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bannerPosition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerPositionExists(bannerPosition.Id))
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
            return View(bannerPosition);
        }

        // GET: Admin/BannerPositions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.BannerPosition == null)
            {
                return NotFound();
            }

            var bannerPosition = await _context.BannerPosition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bannerPosition == null)
            {
                return NotFound();
            }

            return View(bannerPosition);
        }

        // POST: Admin/BannerPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.BannerPosition == null)
            {
                return Problem("Entity set 'AppDbContext.BannerPosition'  is null.");
            }
            var bannerPosition = await _context.BannerPosition.FindAsync(id);
            if (bannerPosition != null)
            {
                _context.BannerPosition.Remove(bannerPosition);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerPositionExists(Guid id)
        {
          return (_context.BannerPosition?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
