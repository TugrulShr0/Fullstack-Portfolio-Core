using Core_Proje.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<WriterRole> _roleManager;
        private readonly UserManager<WriterUser> _userManager;

        public RoleController(RoleManager<WriterRole> roleManager, UserManager<WriterUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel p)
        {
            if (ModelState.IsValid)
            {
                WriterRole role = new WriterRole
                {
                    Name = p.RoleName
                };

                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(p);
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var value = await _roleManager.FindByIdAsync(id.ToString());
            if (value != null)
            {
                await _roleManager.DeleteAsync(value);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return RedirectToAction("UserList");
            }

            TempData["UserId"] = user.Id;
            ViewBag.UserName = $"{user.Name} {user.Surname} ({user.UserName})";

            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            List<UserRoleAssignViewModel> model = new List<UserRoleAssignViewModel>();
            foreach (var item in roles)
            {
                UserRoleAssignViewModel m = new UserRoleAssignViewModel
                {
                    RoleId = item.Id,
                    RoleName = item.Name,
                    RoleExist = userRoles.Contains(item.Name)
                };
                model.Add(m);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(List<UserRoleAssignViewModel> model)
        {
            int userId = (int)TempData["UserId"];
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user != null)
            {
                foreach (var item in model)
                {
                    if (item.RoleExist)
                    {
                        await _userManager.AddToRoleAsync(user, item.RoleName);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                    }
                }
            }

            return RedirectToAction("UserList");
        }
    }
}
