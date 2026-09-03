using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Proje.Areas.Writer.ViewComponents
{
    public class Navbar : ViewComponent
    {
        private readonly UserManager<WriterUser> _userManager;

        public Navbar(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Kullanıcı adı null değilse veritabanından çek
            if (User.Identity.IsAuthenticated && !string.IsNullOrEmpty(User.Identity.Name))
            {
                var values = await _userManager.FindByNameAsync(User.Identity.Name);
                if (values != null && !string.IsNullOrEmpty(values.ImageUrl))
                {
                    ViewBag.v = values.ImageUrl;
                }
                else
                {
                    ViewBag.v = "default-profile.png"; // Resim alanı boşsa atanacak varsayılan resim
                }
            }
            else
            {
                ViewBag.v = "default-profile.png"; // Oturum açılmamışsa atanacak varsayılan resim
            }

            return View();
        }
    }
}