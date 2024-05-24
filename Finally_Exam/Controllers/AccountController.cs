using Finally_Exam.Models;
using Finally_Exam.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Finally_Exam.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {

            if (!ModelState.IsValid) return View();

            User newUser = new User()
            {
                Name = registerVm.Name,
                Email = registerVm.Email,
                Surname = registerVm.Surname,
                UserName = registerVm.UserName,
            };
            var Regs = await _userManager.CreateAsync(newUser, registerVm.Password);
            if (!Regs.Succeeded)
            {
                foreach (var item in Regs.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            return View("Login");
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
