using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models;
using TechMed.Areas.Admin.Models.Banner;
using TechMed.Areas.Admin.Models.Recruitment;

namespace TechMed.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _context;

		public HomeController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public async Task<List<Banner>> GetBannersAtPosition(string positionCode)
		{

			var banners = await _context.Banner
				 .Include(b => b.BannerPosition)
				 .Where(b => b.BannerPosition.Code == positionCode)
				 .ToListAsync();

			return banners;
		}
	}
}
