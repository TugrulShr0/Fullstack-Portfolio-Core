using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class AboutController : Controller
    {
        private readonly AboutManager aboutManager = new AboutManager(new EfAboutDal());

        [HttpGet]
        public IActionResult Index()
        {
            var values = aboutManager.TGetByID(1);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> Index(About about, IFormFile? ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var extension = Path.GetExtension(ImageFile.FileName);
                var imageName = Guid.NewGuid() + extension;
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userimage/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, imageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                about.ImageUrl = "/userimage/" + imageName;
            }

            aboutManager.TUpdate(about);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}