using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models;
using TechMed.Areas.Admin.Models.Banner;

namespace TechMed.Service
{
    public class BannerService
    {
        private readonly AppDbContext _context;

        public BannerService(AppDbContext context)
        {
            _context = context;
        }

        public List<Banner> GetBannerByPositionCode(string bannerPostition)
        {
            if (_context.Banner == null && _context.BannerPosition == null)
            {
                return new List<Banner>();
            }

            DateTime currentDate = DateTime.Now;

            var bannerSection1 = _context.Banner
            .Include(b => b.BannerPosition)
            .Where(b => b.BannerPosition.Code == bannerPostition &&
                        b.EndDate > currentDate &&
                        b.Status == 1).
						OrderBy(b => b.Order).ToList();
            return bannerSection1;
        }
    }
}
