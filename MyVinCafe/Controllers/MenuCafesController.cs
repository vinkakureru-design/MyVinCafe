using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyVinCafe.Data;
using MyVinCafe.Models;

namespace MyVinCafe.Controllers
{
    public class MenuCafesController : Controller
    {
        private readonly AppDbContext _context;

        public MenuCafesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MenuCafes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MenuCafe.ToListAsync());
        }

        // GET: MenuCafes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuCafes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NamaMenu,Deskripsi,Harga,Kategori,Diskon")] MenuCafe menuCafe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuCafe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuCafe);
        }

        // GET: MenuCafes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCafe = await _context.MenuCafe.FindAsync(id);
            if (menuCafe == null)
            {
                return NotFound();
            }
            return View(menuCafe);
        }

        // POST: MenuCafes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaMenu,Deskripsi,Harga,Kategori,Diskon")] MenuCafe menuCafe)
        {
            if (id != menuCafe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuCafe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuCafeExists(menuCafe.Id))
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
            return View(menuCafe);
        }

        // GET: MenuCafes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCafe = await _context.MenuCafe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuCafe == null)
            {
                return NotFound();
            }

            return View(menuCafe);
        }

        // POST: MenuCafes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuCafe = await _context.MenuCafe.FindAsync(id);
            if (menuCafe != null)
            {
                _context.MenuCafe.Remove(menuCafe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuCafeExists(int id)
        {
            return _context.MenuCafe.Any(e => e.Id == id);
        }
    }
}
