using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.Banner;
using TechMed.Areas.Admin.Models.News;
using X.PagedList;

namespace TechMed.Controllers
{
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;

        public NewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index(int? page)
        {
            if(_context.News != null)
            {
                int pageSize = 5; //SL phan tu tren 1 trang
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
                else
                {
                    return Problem("Entity set 'AppDbContext.News' is null.");
                }
            }
            return View();
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.NewsCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        
    }
}
