using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.News;
using TechMed.Areas.Admin.Models.Recruitment;

namespace TechMed.Service
{
    public class NewsService
    {
        private readonly AppDbContext _context;

        public NewsService(AppDbContext context)
        {
            _context = context;
        }
        public List<News> GetLastNews()
        {
            if (_context.News == null)
            {
                return new List<News>();
            }
            // Lấy danh sách menu từ cơ sở dữ liệu và sắp xếp chúng theo thứ tự tăng dần
            var latestNews = _context.News.OrderByDescending(news => news.CreatedDate).Take(5).ToList();

            return latestNews;
        }
        //Lấy ra tin tức dạng trang chủ
		public List<News> GetHomeNews()
		{
			if (_context.News == null)
			{
				return new List<News>();
			}
			// Lấy danh sách menu từ cơ sở dữ liệu và sắp xếp chúng theo thứ tự tăng dần
			var homeNews = _context.News
                .Where(news => news.Type == 1)
				.OrderBy(news => news.Order)
				.ToList();
			return homeNews;
		}
		//Lấy ra tin tức hot
		public List<News> GetHotNews(int? qty)
		{
			if (_context.News == null)
			{
				return new List<News>();
			}
			// Lấy danh sách menu từ cơ sở dữ liệu và sắp xếp chúng theo thứ tự tăng dần
			var hotNews = _context.News
			.Where(news => news.Type == 2)
			.OrderBy(news => news.Order)
			.ToList();

			if (qty.HasValue && qty > 0)
			{
				hotNews = hotNews.Take(qty.Value).ToList();
			}

			return hotNews;
		}
		//Lấy ra tin tức loại mới
		public List<News> GetNewsTypeNew(int? qty)
		{
			if (_context.News == null)
			{
				return new List<News>();
			}
			// Lấy danh sách menu từ cơ sở dữ liệu và sắp xếp chúng theo thứ tự tăng dần
			var hotNews = _context.News
				.Where(news => news.Type == 3)
				.OrderBy(news => news.Order)
				.ToList();
            if (qty.HasValue && qty > 0)
            {
                hotNews = hotNews.Take(qty.Value).ToList();
            }
            return hotNews;
		}

        //Lấy ra tin tức cùng cate với tin đang xem chi tiết - dùng cho trang chi tiết tuyển dụng
        public List<News> GetRelateNews(Guid? cateId, int? qty)
        {
            if (_context.News == null)
            {
                return new List<News>();
            }
            // Lấy danh sách menu từ cơ sở dữ liệu và sắp xếp chúng theo thứ tự tăng dần
            var newsRelate = _context.News.OrderBy(news => news.Order).ToList();
            if (cateId != null)
            {
                newsRelate = newsRelate.Where(news => news.NewsCategoryId == cateId).ToList();
            }

            if (qty.HasValue && qty > 0)
            {
                newsRelate = newsRelate.Take(qty.Value).ToList();
            }
            return newsRelate;
        }
    }
}
