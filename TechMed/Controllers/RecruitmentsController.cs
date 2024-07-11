using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.Recruitment;
using X.PagedList;

namespace TechMed.Controllers
{
    public class RecruitmentsController : Controller
    {
        private readonly AppDbContext _context;

        public RecruitmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Recruitments
        public async Task<IActionResult> Index(int? page)
        {
            if (_context.Recruitments != null)
            {
                int pageSize = 4; //SL phan tu tren 1 trang
                int pageNumber = (page ?? 1);
                var recruitments = await _context.Recruitments
                            .Include(r => r.RecruitmentCate).ToListAsync();
                IPagedList<Recruitment> recruitmentsPaging = recruitments.ToPagedList(pageNumber, pageSize);
                if (recruitments != null)
                {
                    return View(recruitmentsPaging);
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Recruitments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (_context.Recruitments != null)
            {
                var recruitment = await _context.Recruitments
                .Include(r => r.RecruitmentCate)
                .Include(r => r.RecruitmentTags).ThenInclude(r => r.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
                if (recruitment == null)
                {
                    return NotFound();
                }
                return View(recruitment);
            }
            return View();
        }

    }
}
