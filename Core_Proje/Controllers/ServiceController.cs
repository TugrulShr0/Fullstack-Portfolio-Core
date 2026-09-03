using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Core_Proje.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core_Proje.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class ServiceController : Controller
    {
        private readonly ServiceManager serviceManager = new ServiceManager(new EfServiceDal());

        public IActionResult Index()
        {
            var values = serviceManager.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddService(Service service, IFormFile? ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                service.ImageUrl = await SaveImageAsync(ImageFile);
            }

            serviceManager.TAdd(service);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteService(int id)
        {
            var values = serviceManager.TGetByID(id);
            if (values != null)
            {
                FileHelper.DeleteFile(values.ImageUrl);
                serviceManager.TDelete(values);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditService(int id)
        {
            var values = serviceManager.TGetByID(id);
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(Service service, IFormFile? ImageFile)
        {
            var current = serviceManager.TGetByID(service.ServiceID);

            if (current != null)
            {
                current.Title = service.Title;

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    FileHelper.DeleteFile(current.ImageUrl);
                    current.ImageUrl = await SaveImageAsync(ImageFile);
                }

                serviceManager.TUpdate(current);
            }

            return RedirectToAction("Index");
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var imageName = Guid.NewGuid() + extension;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userimage/");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, imageName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/userimage/" + imageName;
        }
    }
}