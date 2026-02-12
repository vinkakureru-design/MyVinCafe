using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVinCafe.Data;
using MyVinCafe.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json;

namespace MyVinCafe.Controllers
{
    //defaultnya kalo masuk web
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

        // Method untuk halaman Kaffee (Kopi)
        public async Task<IActionResult> Kaffe()
        {
            // Mengambil data dari konteks database 
            // Filter hanya yang kategorinya 'Coffee'
            var kaffeeMenu = await _context.MenuCafe
                .Where(m => m.Kategori == "Kopi")
                .ToListAsync();

            return View(kaffeeMenu);
        }

        // Method untuk halaman Kaffee (Kopi)
        public async Task<IActionResult> GETRÄNKE()
        {
            // Mengambil data dari konteks database 
            // Filter hanya yang kategorinya 'Coffee'
            var GETRÄNKE = await _context.MenuCafe
                .Where(m => m.Kategori == "Non-Kopi")
                .ToListAsync();

            return View(GETRÄNKE);
        }

        // Method untuk halaman Kaffee (Kopi)
        public async Task<IActionResult> GEBÄCK()
        {
            // Mengambil data dari konteks database 
            // Filter hanya yang kategorinya 'Coffee'
            var GEBÄCK = await _context.MenuCafe
                .Where(m => m.Kategori == "Roti-Kue")
                .ToListAsync();

            return View(GEBÄCK);
        }






        // logika keranjang belanja ada di sini ya

        public async Task<IActionResult> TambahKeranjang(int id)
        {
            var menuItem = await _context.MenuCafe.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            var sessionData = HttpContext.Session.GetString("Keranjang");
            List<Keranjang> keranjang = sessionData == null
                ? new List<Keranjang>()
                : JsonSerializer.Deserialize<List<Keranjang>>(sessionData)!;

            var existingItem = keranjang.FirstOrDefault(k => k.MenuId == id);
            if (existingItem != null)
            {
                existingItem.Jumlah++;
            }
            else
            {
                keranjang.Add(new Keranjang
                {
                    MenuId = menuItem.Id,
                    NamaMenu = menuItem.NamaMenu,
                    Harga = (int)menuItem.Harga,
                    Jumlah = 1
                });
            }

            HttpContext.Session.SetString("Keranjang", JsonSerializer.Serialize(keranjang));
            return RedirectToAction("Kaffe", "Home");
        }

        public IActionResult Keranjang()
        {
            var sessionData = HttpContext.Session.GetString("Keranjang");
            
            List<Keranjang> keranjang = sessionData == null
                ? new List<Keranjang>()
                : JsonSerializer.Deserialize<List<Keranjang>>(sessionData)!;    

            return View(keranjang);
        }

        public IActionResult BersihkanKeranjang()
        {
            HttpContext.Session.Remove("Keranjang");
            return RedirectToAction("Keranjang");
        }
    }
}
