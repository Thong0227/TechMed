using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.News;
using X.PagedList;

namespace TechMed.Controllers
{
    public class NewsCateController : Controller
    {
        private readonly AppDbContext _context;

        public NewsCateController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(Guid? id, int? page)
        {
            if (id == null || _context.NewsCategory == null)
            {
                return NotFound();
            }
            int pageSize = 5; //SL phan tu tren 1 trang
            int pageNumber = (page ?? 1);
            var newsCategory = await _context.NewsCategory
                         .Include(nc => nc.News) // Bao gồm danh sách các bài viết
                         .FirstOrDefaultAsync(m => m.Id == id);
            if (newsCategory == null)
            {
                return NotFound();
            }


			IPagedList<News> newsPaging = newsCategory.News.OrderByDescending(n => n.CreatedDate).ToPagedList(pageNumber, pageSize);
            ViewBag.NewsCategoryName = newsCategory.Name;
            ViewBag.NewsCategoryId = newsCategory.Id;

            return View(newsPaging);
        }
    }
}
