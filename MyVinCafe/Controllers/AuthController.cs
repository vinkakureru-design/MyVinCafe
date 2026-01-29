using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVinCafe.Data;
using MyVinCafe.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyVinCafe.Controllers
{
    public class AuthController : Controller
    {
        // buat membaca database (_context)
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Register(string fullName, string username, string password) // membuat task/tugas Register yang berisi nama, username. dan password
        {
            //Cek apakah username sudah ada
            if (await _context.Users.AnyAsync(u => u.Username == username)) // logika buat nyari kalau username ada yang sama di database lalu kirim pesan error
            {
                TempData["Error"] = "username sudah digunakan";
                return RedirectToAction("Index", "Home");
            }

            var newUser = new User // buat user baru dan ngambil data yang dibutuhin dari models
            {
                FullName = fullName,
                Username = username,
                Password = password, // PESAN: Kalau ada kesempatan belajar Hashing (Bcrypt) atau ubah jadi kode acak
                Role = "Member" //Defaultnya member
            };

            _context.Users.Add(newUser); // minta database tambah user baru
            await _context.SaveChangesAsync(); // simpan perubahan

            return RedirectToAction("Index", "Home"); // direct halaman ke /home/indx
        }


        //logika buat login
        [HttpPost]


        public async Task<IActionResult> Login(string username, string password)//buat tugas login yang isinya username dan password dan meminta dari models (username buat kode unik yang bisa dibedain dari semua orang)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password); // logika AND, kalo dua-duanya bener ya benerlah, kalo satu salah ya error

            if (user == null) //logika kalo user gaada
            {
                TempData["Error"] = "username atau password salah!";
                return RedirectToAction("Index", "Home");
            }

            // Membuat identitas (CLaims)? apa jir logika Claims??? belajar lagi ( > /\ < )
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("FullName", user.FullName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync
                (CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            // Mengarahkan berdasarkan role default atau dipilihin sama gw
            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "Admin"); //direct kalo rolenya == admin
            }

            return RedirectToAction("Index", "Home");//kalo bukan admin, gw adminya

        }

        // logika logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); // balik ke /home/index
        }
    }
}


// pesan: belajar pake cookies. kamu mau kukiss ;P