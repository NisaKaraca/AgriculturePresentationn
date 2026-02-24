using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AgriculturePresentationn.Models; 

namespace AgriculturePresentationn.Controllers
{
    [Authorize] 
    public class ProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var username = User.Identity?.Name;

            if (username == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var values = await _userManager.FindByNameAsync(username);

            UserEditViewModel userEditViewModel = new UserEditViewModel();
            userEditViewModel.Mail = values.Email;
            userEditViewModel.Phone = values.PhoneNumber;

            return View(userEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p)
        {
            var username = User.Identity?.Name;

            if (username == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var values = await _userManager.FindByNameAsync(username);

            if (p.Password == p.ConfirmPassword)
            {
                values.Email = p.Mail;
                values.PhoneNumber = p.Phone;
                values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, p.Password);

                var result = await _userManager.UpdateAsync(values);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            return View();
        }
    }
}
