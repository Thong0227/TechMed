using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.Banner;
using TechMed.Areas.Admin.Models.News;
using X.PagedList;

namespace TechMed.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NewsCateController : Controller
    {
        private readonly AppDbContext _context;

        public NewsCateController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/NewsCate
        public async Task<IActionResult> Index(int? page)
        {
			int pageSize = 10; //SL phan tu tren 1 trang
			int pageNumber = (page ?? 1);
            if(_context.NewsCategory == null)
            {
				return Problem("Entity set 'AppDbContext.Banner' is null.");
			}
            var newsCates = await _context.NewsCategory.ToListAsync();
			IPagedList<NewsCategory> newsCatesPaging = newsCates.ToPagedList(pageNumber, pageSize);
			if (newsCates != null)
			{
				return View(newsCatesPaging);
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
			if (_context.NewsCategory == null)
			{
				return Problem("Entity set 'AppDbContext.Banner' is null.");
			}
			IQueryable<NewsCategory> newsCatesQuerry = _context.NewsCategory;

			if (!string.IsNullOrEmpty(keyword))
			{
                newsCatesQuerry =  newsCatesQuerry.Where(n => n.Name.Contains(keyword));
			}
			var newsCates = await newsCatesQuerry.ToListAsync();

			IPagedList<NewsCategory> newsCatesPaging = newsCates.ToPagedList(pageNumber, pageSize);
            ViewBag.keyword = keyword;
            if (newsCates != null)
			{
				return View(newsCatesPaging);
			}
			else
			{
				return Problem("Entity set 'AppDbContext.Banner' is null.");
			}
		}

		// GET: Admin/NewsCate/Details/5
		public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.NewsCategory == null)
            {
                return NotFound();
            }

            var newsCategory = await _context.NewsCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsCategory == null)
            {
                return NotFound();
            }

            return View(newsCategory);
        }

        // GET: Admin/NewsCate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NewsCate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] NewsCategory newsCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsCategory);
        }

        // GET: Admin/NewsCate/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.NewsCategory == null)
            {
                return NotFound();
            }

            var newsCategory = await _context.NewsCategory.FindAsync(id);
            if (newsCategory == null)
            {
                return NotFound();
            }
            return View(newsCategory);
        }

        // POST: Admin/NewsCate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] NewsCategory newsCategory)
        {
            if (id != newsCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsCategoryExists(newsCategory.Id))
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
            return View(newsCategory);
        }

        // GET: Admin/NewsCate/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.NewsCategory == null)
            {
                return NotFound();
            }

            var newsCategory = await _context.NewsCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsCategory == null)
            {
                return NotFound();
            }

            return View(newsCategory);
        }

        // POST: Admin/NewsCate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.NewsCategory == null)
            {
                return Problem("Entity set 'AppDbContext.NewsCategory'  is null.");
            }
            var newsCategory = await _context.NewsCategory.FindAsync(id);
            if (newsCategory != null)
            {
                _context.NewsCategory.Remove(newsCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsCategoryExists(Guid id)
        {
          return (_context.NewsCategory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
