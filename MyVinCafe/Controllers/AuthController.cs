using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MyVinCafe.Data;
using MyVinCafe.Models;

namespace MyVinCafe.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // 1. Cek input kosong
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "IDENT_ERROR: Username dan Password wajib diisi.";
                return RedirectToAction("Index", "Home"); // Kembali ke halaman utama (agar modal bisa dipicu lagi)
            }

            // 2. Cari user di database
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // ... Logika Sign-In Cookie seperti sebelumnya ...
                return RedirectToAction("Index", "Home");
            }

            // 3. Jika data tidak ditemukan
            TempData["Error"] = "ACCESS_DENIED: Kode akses atau ID salah.";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string fullName, string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                ModelState.AddModelError("", "Username sudah dipakai.");
                return View();
            }

            var newUser = new User
            {
                FullName = fullName,
                Username = username,
                Password = password, // Untuk belajar, pakai plain text dulu. Untuk produksi harus di-hash!
                Role = "Customer"
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}