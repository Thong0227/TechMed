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
    public class BannersController : Controller
    {
        private readonly AppDbContext _context;

        public BannersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Banners
        public async Task<IActionResult> Index()
        {
              return _context.Banner != null ? 
                          View(await _context.Banner.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Banner'  is null.");
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
        public async Task<IActionResult> Create([Bind("Id,Name,ImageUrl,Status,StartDate,EndDate,BannerPositionId")] Banner banner)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ImageUrl,Status,StartDate,EndDate,BannerPositionId")] Banner banner)
        {
            if (id != banner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return Problem("Entity set 'AppDbContext.Banner'  is null.");
            }
            var banner = await _context.Banner.FindAsync(id);
            if (banner != null)
            {
                _context.Banner.Remove(banner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerExists(Guid id)
        {
          return (_context.Banner?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
