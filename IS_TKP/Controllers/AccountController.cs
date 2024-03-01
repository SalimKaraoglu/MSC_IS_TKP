using ApplicationCore.DTO_s.Account;
using ApplicationCore.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IS_TKP.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }

        [AllowAnonymous]
        public IActionResult Register() => View();

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]                
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser { UserName = model.UserName, Email = model.Email };
                appUser.PasswordHash = _passwordHasher.HashPassword(appUser, model.Password);

                IdentityResult identityResult = await _userManager.CreateAsync(appUser);

                if (identityResult.Succeeded)
                {
                    TempData["Success"] = "Kaydınız yapıldı. Giriş yapabilirsiniz...";
                    return RedirectToAction("Login");
                }
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(model);
                }

            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogIn() => View();

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    // verdiğimiz username ve password database ile uyumlumu  değilmi burada onu kontrol ediyoruz. Bunun sonucunda da signInResult olarak true yada false bir sonuç dönderdi bunu da  if (signInResult.Succeeded) bunun içerisinde döndü
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

                    // Eğer üst satırdaki giriş bilgileri başarılı ise Home'un Index'ine yönlendiriyor.
                    if (signInResult.Succeeded)
                    {
                        TempData["Success"] = "Hoşgeldiniz " + user.UserName;
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["Error"] = "Kullanıcı adı veya şifre yanlış";
                    ModelState.AddModelError("", "Giriş Bilgileri Yanlış!");
                    return View(model);
                }
            }
            TempData["Error"] = "Lütfen aşağıdaki kuralara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> EditUser()
        {
            var appUserID = _userManager.GetUserId(User);
            var appUser = await _userManager.FindByIdAsync(appUserID);

            var model = new EditUserDTO(appUser);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var appUserID = _userManager.GetUserId(User);
                var appUser = await _userManager.FindByIdAsync(appUserID);

                appUser.UserName = model.UserName;
                appUser.Email = model.Email;

                if (model.Password != null)
                {
                    appUser.PasswordHash = _passwordHasher.HashPassword(appUser, model.Password);
                }
                IdentityResult identityResult = await _userManager.UpdateAsync(appUser);

                if (identityResult.Succeeded)
                {
                    TempData["Success"] = "Profiliniz Güncellendi!";
                }
                else
                {
                    TempData["Error"] = "Profiliniz Güncellenemedi!";
                }
                return View(model);

            }
            TempData["Error"] = "Aşağıdaki bilgileri kontrol ediniz!";
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            TempData["Success"] = "Çıkış Yapıldı İyi Günler Dileriz :)";
            return RedirectToAction("Login");
        }


    }
}
