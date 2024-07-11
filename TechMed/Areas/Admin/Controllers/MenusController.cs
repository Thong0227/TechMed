using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models;
using TechMed.Areas.Admin.Models.News;
using X.PagedList;

namespace TechMed.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MenusController : Controller
    {
        private readonly AppDbContext _context;

        public MenusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Menus
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10; // Số lượng phần tử trên mỗi trang
            int pageNumber = page ?? 1;

            if (_context.Menu == null)
            {
                return Problem("Dữ liệu không tồn tại");
            }

            var menus = await _context.Menu.OrderBy(m => m.ParentId).ThenBy(m => m.Name).ToListAsync();
            var menuTree = BuildMenuTreeIndex(null, menus);

            return View(menuTree.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> Index(string? keyword, int? page)
        {
            int pageSize = 10; //SL phan tu tren 1 trang
            int pageNumber = (page ?? 1);
            if (_context.Menu == null)
            {
                return Problem("Dữ liệu không tồn tại");
            }
            IQueryable<Menu> menuQuerry = _context.Menu;

            if (!string.IsNullOrEmpty(keyword))
            {
                menuQuerry = menuQuerry.Where(n => n.Name.Contains(keyword));
            }
            var menus = await menuQuerry.ToListAsync();

            IPagedList<Menu> menuPaging = menus.ToPagedList(pageNumber, pageSize);
            ViewBag.keyword = keyword;
            if (menus != null)
            {
                return View(menuPaging);
            }
            else
            {
                return Problem("Dữ liệu không tồn tại");
            }
        }

        private List<Menu> BuildMenuTreeIndex(Guid? parentId, List<Menu> menus, string prefix = "")
        {
            var menuTree = new List<Menu>();

            var childMenus = menus.Where(m => m.ParentId == parentId).ToList();
            foreach (var menuOption in childMenus)
            {
                var submenu = BuildMenuTreeIndex(menuOption.Id, menus, prefix + "--");
                menuOption.Name = prefix + " " + menuOption.Name; // Thêm prefix vào tên menu
                menuTree.Add(menuOption);
                menuTree.AddRange(submenu);
            }

            return menuTree;
        }

        // GET: Admin/Menus/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Admin/Menus/Create
        public async Task<IActionResult> Create()
        {
            // Trả về danh sách Menu để chọn Menu cha
            if (_context.Menu == null)
            {
                return Problem("Dữ liệu không tồn tại");
            }

            var menus = await _context.Menu.OrderBy(m => m.ParentId).ThenBy(m => m.Name).ToListAsync();
            var menuTree = BuildMenuTree(null, menus);

            ViewBag.ParentMenus = menuTree;
            return View();
        }

        private List<SelectListItem> BuildMenuTree(Guid? parentId, List<Menu> menus, string prefix = "")
        {
            var menuTree = new List<SelectListItem>();

            var childMenus = menus.Where(m => m.ParentId == parentId).ToList();
            foreach (var menuOption in childMenus)
            {
                var submenu = BuildMenuTree(menuOption.Id, menus, prefix + "--");
                menuTree.Add(new SelectListItem { Value = menuOption.Id.ToString(), Text = prefix + " " + menuOption.Name });
                menuTree.AddRange(submenu);
            }

            return menuTree;
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Order,Status,Link,ParentId")] Menu menu)
        {
			if (ModelState.IsValid)
			{
				menu.Id = Guid.NewGuid();
				_context.Add(menu);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(menu);
		}

        // GET: Admin/Menus/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.FindAsync(id);
			var menusList = await _context.Menu.OrderBy(m => m.ParentId).ThenBy(m => m.Name).ToListAsync();
			var menuTree = BuildMenuTree(null, menusList);

			ViewBag.ParentMenus = menuTree;
			if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Order,Status,Link,ParentId")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Admin/Menus/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Menu == null)
            {
                return Problem("Entity set 'AppDbContext.Menu'  is null.");
            }
            var menu = await _context.Menu.FindAsync(id);
            if (menu != null)
            {
                _context.Menu.Remove(menu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(Guid id)
        {
          return (_context.Menu?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
