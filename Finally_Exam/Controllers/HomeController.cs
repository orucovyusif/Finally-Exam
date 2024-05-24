using Finally_Exam.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Finally_Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.explore.ToList());
        }

      
    }
}
