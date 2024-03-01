using ApplicationCore.DTO_s.Roles;
using ApplicationCore.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IS_TKP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private string id;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult CreateRole() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(model.RoleName);
                IdentityResult identityResult = await _roleManager.CreateAsync(role);

                if (identityResult.Succeeded)
                {
                    TempData["Success"] = "Rol başarılı şekilde eklenmiştir!";
                    return RedirectToAction("Index");
                }
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    TempData["Error"] = "Birşeyler Ters Gitti!";
                    return View(model);
                }
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!!";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AssignedUser(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                TempData["Error"] = "Role bulunamadı!";
                return RedirectToAction("Index");
            }

            List<AppUser> hasRole = new List<AppUser>();
            List<AppUser> hasNotRole = new List<AppUser>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? hasRole : hasNotRole;
                list.Add(user);

            }

            AssignedRoleDTO model = new AssignedRoleDTO
            {
                Role = role,
                RoleName = role.Name,
                HasNotRole = hasNotRole,
                HasRole = hasRole,
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignedUser(AssignedRoleDTO model)
        {
            IdentityResult identityResult = new IdentityResult();

            foreach (var userId in model.AddIds ?? new string[] { })
            {
                AppUser user = await _userManager.FindByIdAsync(userId);
                identityResult = await _userManager.AddToRoleAsync(user, model.RoleName);
            }
            foreach (var userId in model.DeleteIds ?? new string[] { })
            {
                AppUser user = await _userManager.FindByIdAsync(userId);
                identityResult = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            }
            if (identityResult.Succeeded)
            {
                TempData["Success"] = "Yaptığınız İşlemler Kaydedildi!";
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditRole(string id)
        {

            var appRole = await _roleManager.FindByIdAsync(id);

            var model = new EditRoleDTO(appRole);

            return View(model);

        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(EditRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                var appRole = await _roleManager.FindByIdAsync(model.Id);

                appRole.Name = model.RoleName;

                IdentityResult identityResult = await _roleManager.UpdateAsync(appRole);

                if (identityResult.Succeeded)
                {
                    TempData["Success"] = "Role Güncellenmiştir!";
                }
                else
                {
                    TempData["Error"] = "Role Güncellenememiştir!";
                }
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Aşağıdaki Kurallara Uyunuz!";
            return View(model);
        }
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
                TempData["Success"] = "Role silindi!";
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }

}
