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
    public class ContactsController : Controller
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Contacts
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10; //SL phan tu tren 1 trang
            int pageNumber = (page ?? 1);
            if (_context.Contact == null)
            {
                return Problem("Entity set 'AppDbContext.Contact' is null.");
            }
            var contacts = await _context.Contact.ToListAsync();
            IPagedList<Contact> contactPaging = contacts.ToPagedList(pageNumber, pageSize);
            if (contacts != null)
            {
                return View(contactPaging);
            }
            else
            {
                return Problem("Entity set 'AppDbContext.Banner' is null.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Index(string? keyword, int? page)
        {
            int pageSize = 10; //SL phan tu tren 1 trang
            int pageNumber = (page ?? 1);
            if (_context.Contact == null)
            {
                return Problem("Entity set 'AppDbContext.Contact' is null.");
            }
            IQueryable<Contact> contactQuerry = _context.Contact;

            if (!string.IsNullOrEmpty(keyword))
            {
                contactQuerry = contactQuerry.Where(n => n.Name.Contains(keyword));
            }
            var contacts = await contactQuerry.ToListAsync();

            IPagedList<Contact> contactPaging = contacts.ToPagedList(pageNumber, pageSize);
            ViewBag.keyword = keyword;
            if (contacts != null)
            {
                return View(contactPaging);
            }
            else
            {
                return Problem("Entity set 'AppDbContext.Contact' is null.");
            }
        }
        // GET: Admin/Contacts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Admin/Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Content")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Admin/Contacts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Email,Content")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
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
            return View(contact);
        }

        // GET: Admin/Contacts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Admin/Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Contact == null)
            {
                return Problem("Entity set 'AppDbContext.Contact'  is null.");
            }
            var contact = await _context.Contact.FindAsync(id);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(Guid id)
        {
          return (_context.Contact?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
