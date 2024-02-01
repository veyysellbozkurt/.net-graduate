using LoginSolo.Dtos;
using LoginSolo.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSolo.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            if (ModelState.IsValid)

            {
                AppUser appUser = new AppUser()
                {
                    UserName = appUserRegisterDto.Name,
                    Email = appUserRegisterDto.Email,
                };

                var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);

                if (result.Succeeded)
                {
                    return View("RegisterCompleted");
                }

                foreach (var error in result.Errors)
                {
                    if (error.Code == "PasswordRequiresDigit")
                    {
                        ModelState.AddModelError(string.Empty, "Şifrenizde en az bir sayı olmalıdır.");
                    }
                    else if (error.Code == "PasswordRequiresUpper")
                    {
                        ModelState.AddModelError(string.Empty, "Şifrenizde en az bir büyük harf olmalıdır.");
                    }
                    else if (error.Code == "PasswordRequiresLower")
                    {
                        ModelState.AddModelError(string.Empty, "Şifrenizde en az bir küçük harf olmalıdır.");
                    }
                    else if (error.Code == "PasswordRequiresNonAlphanumeric")
                    {
                        ModelState.AddModelError(string.Empty, "Şifrenizde en az bir özel karakter olmalıdır.");
                    }
                    else if (error.Code == "PasswordTooShort")
                    {
                        ModelState.AddModelError(string.Empty, "Şifreniz en az 7 karakter olmalıdır.");
                    }
                }
            }

            return View(appUserRegisterDto);
        }

    }
}
