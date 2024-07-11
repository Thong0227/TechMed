using TechMed.Areas.Admin.Data;
using TechMed.Areas.Admin.Models;

namespace TechMed.Service
{
    public class MenuService
    {
        private readonly AppDbContext _context;

        public MenuService(AppDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách menu cha từ cơ sở dữ liệu và sắp xếp chúng theo thứ tự tăng dần
        public List<Menu> GetMenus()
        {
            if(_context.Menu == null) { 
                return new List<Menu>();
            }
            return _context.Menu.OrderBy(menu => menu.Order)
                .Where(menu => menu.Status == 1 && menu.ParentId == null)
                .ToList();
        }

        public List<Menu> GetSubMenus(Guid parentId)
        {
            // Lấy danh sách menu con từ cơ sở dữ liệu có parentId trùng với menuId cha
            if (_context.Menu == null)
            {
                return new List<Menu>();
            }
            return _context.Menu
                .Where(menu => menu.ParentId == parentId && menu.Status == 1)
                .OrderBy(menu => menu.Order)
                .ToList();
        }
    }
}
