using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVinCafe.Data;
using MyVinCafe.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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

        public IActionResult Kaffe()
        {
            return View();
        }
    }
}
