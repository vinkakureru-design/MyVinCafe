using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyVinCafe.Controllers
{
    //control gaada isi
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
