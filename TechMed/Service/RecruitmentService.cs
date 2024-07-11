using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.News;
using TechMed.Areas.Admin.Models.Recruitment;

namespace TechMed.Service
{
    public class RecruitmentService
    {
        private readonly AppDbContext _context;

        public RecruitmentService(AppDbContext context)
        {
            _context = context;
        }
        //Lấy ra tin tuyển dụng cùng cate với tin đang xem chi tiết - dùng cho trang chi tiết tuyển dụng
        public List<Recruitment> GetRelateRecruitments(Guid? cateId, int? qty)
        {
            if (_context.Recruitments == null)
            {
                return new List<Recruitment>();
            }
            // Lấy danh sách menu từ cơ sở dữ liệu và sắp xếp chúng theo thứ tự tăng dần
            var recruitmentsRelate = _context.Recruitments.OrderBy(recruitment => recruitment.Order).ToList();
            if(cateId != null)
            {
                recruitmentsRelate = recruitmentsRelate.Where(recruitment => recruitment.RecruitmentCateId == cateId).ToList();
            }
            
            if (qty.HasValue && qty > 0)
            {
                recruitmentsRelate = recruitmentsRelate.Take(qty.Value).ToList();
            }
            return recruitmentsRelate;
        }

        public List<Recruitment> GetHotRecruitments(int? qty)
        {
            if (_context.Recruitments == null)
            {
                return new List<Recruitment>();
            }
            // Lấy danh sách menu từ cơ sở dữ liệu và sắp xếp chúng theo thứ tự tăng dần
            var recruitmentsRelate = _context.Recruitments
                .Where(recruitment => recruitment.Type == 2)
                .OrderBy(recruitment => recruitment.Order)
                .ToList();

            if (qty.HasValue && qty > 0)
            {
                recruitmentsRelate = recruitmentsRelate.Take(qty.Value).ToList();
            }
            return recruitmentsRelate;
        }
    }
}
