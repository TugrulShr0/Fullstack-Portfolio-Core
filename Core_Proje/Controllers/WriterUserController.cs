using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class WriterUserController : Controller
    {
        private readonly UserManager<WriterUser> _userManager;

        public WriterUserController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListUser()
        {
            var users = _userManager.Users.Select(x => new
            {
                x.Id,
                x.Name,
                x.Surname,
                x.UserName,
                x.Email
            }).ToList();

            return Json(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(WriterUser p)
        {
            p.ImageUrl = "default-profile.png";
            string password = string.IsNullOrEmpty(p.PasswordHash) ? "Password12*" : p.PasswordHash;

            var result = await _userManager.CreateAsync(p, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(p, "Writer");
                return Ok(new { success = true });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok(new { success = true });
                }
                return BadRequest(result.Errors);
            }
            return NotFound();
        }
    }
}