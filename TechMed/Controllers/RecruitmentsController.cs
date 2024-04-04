using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models.Recruitment;

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
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Recruitments.Include(r => r.RecruitmentCate);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Recruitments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Recruitments == null)
            {
                return NotFound();
            }

            var recruitment = await _context.Recruitments
                .Include(r => r.RecruitmentCate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recruitment == null)
            {
                return NotFound();
            }

            return View(recruitment);
        }

    }
}
