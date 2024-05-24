using Finally_Exam.Data;
using Finally_Exam.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finally_Exam.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ExploreController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ExploreController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View(_context.explore.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Explore explore)
        {
            if (!ModelState.IsValid)  return View(); 
            if (!explore.ImgFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImgFile", "Duzgun daxil edin");
            }
            string path = _environment.WebRootPath + @"\Upload\";
            string filename = Guid.NewGuid() + explore.ImgFile.FileName;
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                explore.ImgFile.CopyTo(stream);
            }

            explore.ImgUrl = filename;
            _context.explore.Add(explore);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            var pro = _context.explore.FirstOrDefault(x => x.Id == id);
            if (pro == null)
            {
                return NotFound();
            }
            return View(pro);
        }
        [HttpPost]
        public IActionResult Update(Explore explore)
        {
            if (!ModelState.IsValid && explore.ImgFile != null)
            {
                return View();
            }
            var oldExpt = _context.explore.FirstOrDefault(y => y.Id ==  explore.Id);
            if (explore.ImgFile != null)
            {
                string path = _environment.WebRootPath + @"\Upload\";
                FileInfo fileInfo = new FileInfo(path + oldExpt.ImgUrl);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                string filename = Guid.NewGuid() + explore.ImgFile.FileName;

                using (FileStream stream = new FileStream(path + filename, FileMode.Create))
                {
                    explore.ImgFile.CopyTo(stream);
                }
                oldExpt.ImgUrl = filename;

            }
            oldExpt.Name = explore.Name;
            oldExpt.Description = explore.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var Expt = _context.explore.FirstOrDefault(x => x.Id == id);
            _context.explore.Remove(Expt);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
