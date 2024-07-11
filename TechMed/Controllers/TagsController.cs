using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;

namespace TechMed.Controllers
{
    public class TagsController : Controller
    {
        private readonly AppDbContext _context;

        public TagsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult TagRelate(Guid id)
        {
            if(_context.Tags == null)
            {
                return View();
            }
            var tag = _context.Tags
            .Include(t => t.RecruitmentTags)
            .ThenInclude(rt => rt.Recruitment)
            .FirstOrDefault(t => t.Id == id);

            if (tag == null)
            {
                // Trả về NotFound nếu không tìm thấy tag
                return NotFound();
            }

            // Lấy ra danh sách các recruitment có chứa tag có id như id được truyền vào
            var recruitments = tag.RecruitmentTags
                .Select(rt => rt.Recruitment)
                .ToList();

            return View(recruitments);
        }
    }
}
