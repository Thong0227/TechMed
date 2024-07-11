using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models;
using TechMed.Areas.Admin.Models.Recruitment;

namespace TechMed.Service
{
    public class TagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public List<Tag> GetListTags()
        {
            if (_context.Tags == null)
            {
                return new List<Tag>();
            }
            // Lấy danh sách menu từ cơ sở dữ liệu và sắp xếp chúng theo thứ tự tăng dần
            return _context.Tags.ToList();
        }
    }
}
