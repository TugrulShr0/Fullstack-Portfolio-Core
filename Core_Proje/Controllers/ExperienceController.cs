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
    public class ExperienceController : Controller
    {
        private readonly ExperienceManager experienceManager = new ExperienceManager(new EfExperienceDal());

        public IActionResult Index()
        {
            var values = experienceManager.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddExperience()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExperience(Experience experience, IFormFile Picture)
        {
            if (Picture != null && Picture.Length > 0)
            {
                var extension = Path.GetExtension(Picture.FileName);
                var newImageName = Guid.NewGuid() + extension;

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/experienceimages");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var saveLocation = Path.Combine(folderPath, newImageName);
                using (var stream = new FileStream(saveLocation, FileMode.Create))
                {
                    await Picture.CopyToAsync(stream);
                }

                experience.ImageUrl = "/experienceimages/" + newImageName;
            }
            else
            {
                experience.ImageUrl = "/corona-free-dark-bootstrap-admin-template-1.0.0/template/assets/images/faces/face1.jpg";
            }

            experienceManager.TAdd(experience);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteExperience(int id)
        {
            var values = experienceManager.TGetByID(id);
            if (values != null)
            {
                FileHelper.DeleteFile(values.ImageUrl);
                experienceManager.TDelete(values);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditExperience(int id)
        {
            var values = experienceManager.TGetByID(id);
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExperience(Experience experience, IFormFile Picture)
        {
            var currentExperience = experienceManager.TGetByID(experience.ExperinceID);

            if (currentExperience != null)
            {
                currentExperience.Name = experience.Name;
                currentExperience.Date = experience.Date;
                currentExperience.Description = experience.Description;

                if (Picture != null && Picture.Length > 0)
                {
                    FileHelper.DeleteFile(currentExperience.ImageUrl);

                    var extension = Path.GetExtension(Picture.FileName);
                    var newImageName = Guid.NewGuid() + extension;

                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/experienceimages");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var saveLocation = Path.Combine(folderPath, newImageName);
                    using (var stream = new FileStream(saveLocation, FileMode.Create))
                    {
                        await Picture.CopyToAsync(stream);
                    }

                    currentExperience.ImageUrl = "/experienceimages/" + newImageName;
                }

                experienceManager.TUpdate(currentExperience);
            }

            return RedirectToAction("Index");
        }
    }
}