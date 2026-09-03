using Core_Proje.Areas.Writer.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core_Proje.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]")]
    [AllowAnonymous]
    public class ProfileController : Controller
    {
        private readonly UserManager<WriterUser> _userManager;

        public ProfileController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            if (values == null) return RedirectToAction("Index", "Login");

            UserEditViewModel model = new UserEditViewModel
            {
                Name = values.Name,
                Surname = values.Surname,
                PictureURL = values.ImageUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return RedirectToAction("Index", "Login");

            if (p.Picture != null)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userimage");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var extension = Path.GetExtension(p.Picture.FileName);
                var imageName = Guid.NewGuid() + extension;
                var saveLocation = Path.Combine(folderPath, imageName);

                using (var stream = new FileStream(saveLocation, FileMode.Create))
                {
                    await p.Picture.CopyToAsync(stream);
                }

                user.ImageUrl = imageName;
            }

            user.Name = p.Name;
            user.Surname = p.Surname;

            if (!string.IsNullOrEmpty(p.Password))
            {
                if (string.IsNullOrEmpty(p.CurrentPassword))
                {
                    ModelState.AddModelError("", "Şifrenizi değiştirmek için mevcut şifrenizi girmelisiniz.");
                    p.PictureURL = user.ImageUrl;
                    return View(p);
                }

                if (p.Password != p.PasswordConfirm)
                {
                    ModelState.AddModelError("", "Yeni şifreler birbiriyle eşleşmiyor.");
                    p.PictureURL = user.ImageUrl;
                    return View(p);
                }

                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, p.CurrentPassword, p.Password);
                if (!passwordChangeResult.Succeeded)
                {
                    foreach (var error in passwordChangeResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    p.PictureURL = user.ImageUrl;
                    return View(p);
                }
            }

            // Temel Bilgileri Veritabanına Güncelle (EKLENDİ)
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                p.PictureURL = user.ImageUrl;
                return View(p);
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}